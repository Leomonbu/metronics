/* global cy */

describe('Login funcional', () => {
  it('Debe loguear con credenciales válidas', () => {
    cy.visit('http://localhost:3000/login');

    cy.get('input[name="emailEstudiante"]').type('leo@email.com');
    cy.get('input[name="password"]').type('1234567890');

    cy.get('button[type="submit"]').click();

    cy.contains('Leonardo').should('exist');
  });
});