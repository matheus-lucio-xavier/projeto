import api from './api';

export const login = async (email: string, password: string) => {
  const response = await api.post('/Auth/login', {
    Email: email,
    Password: password,
  });

  return response;
};

export const register = async (nome: string, email: string, password: string) => {
  const response = await api.post('/Auth/register', {
    Nome: nome,
    Email: email,
    Password: password,
  });

  return response;
};