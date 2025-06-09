using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSRegistro.Controllers;
using WSRegistro.DTOs;
using WSRegistro.Services;

namespace WSRegistro.Tests.Controllers
{
    public class EstudiantesControllerTests
    {
        [Fact]
        public async Task Login_CredencialesValidas_RetornaToken()
        {
            // Arrange
            var loginDto = new LoginDto
            {
                EmailEstudiante = "leo@email.com",
                Password = "1234567890"
            };

            var mockService = new Mock<EstudianteService>();

            mockService
                .Setup(s => s.LoginAsync(It.Is<LoginDto>(x =>
                    x.EmailEstudiante == loginDto.EmailEstudiante && x.Password == loginDto.Password)))
                .ReturnsAsync("token_simulado");

            var controller = new EstudiantesController(mockService.Object);

            // Act
            var result = await controller.Login(loginDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var data = okResult.Value as dynamic;

            Assert.Equal("token_simulado", data.token);
            Assert.Equal("test@example.com", data.emailEstudiante);
        }

        [Fact]
        public async Task Login_CredencialesInvalidas_RetornaUnauthorized()
        {
            // Arrange
            var loginDto = new LoginDto
            {
                EmailEstudiante = "wrong@example.com",
                Password = "incorrecta"
            };

            var mockService = new Mock<EstudianteService>(null!, null!);
            mockService
                .Setup(s => s.LoginAsync(loginDto))
                .ReturnsAsync((string?)null);

            var controller = new EstudiantesController(mockService.Object);

            // Act
            var result = await controller.Login(loginDto);

            // Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
            var message = unauthorizedResult.Value?.ToString();

            Assert.Contains("Credenciales inválidas", message);
        }
    }
}
