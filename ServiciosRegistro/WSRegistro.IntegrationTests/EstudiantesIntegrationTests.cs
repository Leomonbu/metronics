using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using WSRegistro.DTOs;

namespace WSRegistro.IntegrationTests
{
    public class EstudiantesIntegrationTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public EstudiantesIntegrationTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Login_ReturnsToken_WhenCredentialsAreValid()
        {
            // Arrange – primero registra un usuario válido
            var registerDto = new EstudianteDto
            {
                TipoDocumento = 1,
                DocumentoEstudiante = 123456789,
                NombresEstudiante = "Prueba",
                ApellidosEstudiante = "Estudiante",
                EmailEstudiante = "prueba@test.com",
                Password = "123456700"
            };

            await _client.PostAsJsonAsync("/api/Estudiantes", registerDto);

            var loginDto = new LoginDto
            {
                EmailEstudiante = "prueba@test.com",
                Password = "123456"
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/Estudiantes/login", loginDto);

            // Assert
            response.EnsureSuccessStatusCode(); // 200 OK
            var content = await response.Content.ReadFromJsonAsync<LoginResponse>();
            Assert.NotNull(content?.token);
        }

        private class LoginResponse
        {
            public string token { get; set; }
            public string emailEstudiante { get; set; }
        }
    }
}
