import api from './api';

export const getConversas = async () => {
  const response = await api.get('/Conversa/conversas');

  return response;
};

export const getConversaMensagens = async (conversaId: string) => {
    const response = await api.get(`/Conversa/conversas/${conversaId}/mensagens`);

    return response;
}

type requestMensagem = {
    type: string;
    content: string;
};

export const postConversaMensagens = async (conversaId: string, mensagem: requestMensagem) => {
    const response = await api.post(`/Conversa/conversas/${conversaId}/adcinar-mensagem`, {
        type: mensagem.type,
        content: mensagem.content
    });

    return response;
}