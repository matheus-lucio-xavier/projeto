import api from './api';

export const getUsers = async () => {
  const response = await api.get('/User/usuarios');

  return response;
};

export const getUserConversas = async () => {
  const response = await api.get('/User/usuarios/conversas');

  return response;
};