import { render, screen, fireEvent } from '@testing-library/react';
import Login from '../components/Login';
import * as authService from '../services/authService';

test('debe guardar el token en localStorage si el login es exitoso', async () => {
  // simula postLogin que retorna un token
  jest.spyOn(authService, 'postLogin').mockResolvedValue({ token: 'abc123' });

  render(<Login />);

  fireEvent.change(screen.getByPlaceholderText('Email'), {
    target: { value: 'test@email.com' },
  });

  fireEvent.change(screen.getByPlaceholderText('Password'), {
    target: { value: '123456' },
  });

  fireEvent.click(screen.getByText('Iniciar Sesión'));

  // espera a que la promesa se resuelva
  await screen.findByText('Iniciar Sesión');

  expect(localStorage.getItem('token')).toBe('abc123');
});
