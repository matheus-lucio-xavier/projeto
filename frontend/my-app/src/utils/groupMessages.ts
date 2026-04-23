
type Mensagem = {
  id: string;
  origemId: string;
  type: string;
  content: string;
  isMine: boolean;
  createdAt: string;
};

type DateItem = {
  id: string;
  type: "date";
  date: string;
};

type ListItem = Mensagem | DateItem;

export function groupMessagesByDate(mensagens: Mensagem[] = []): ListItem[]{
  const result: ListItem[] = [];
  let lastDate: string | null = null;

  mensagens.forEach((msg) => {
    const date = new Date(msg.createdAt).toLocaleDateString("pt-BR", {
      day: "2-digit",
      month: "2-digit",
      year: "numeric",
    });

    if (date !== lastDate) {
      result.push({
        id: `date-${date}`,
        type: "date",
        date,
      });
      lastDate = date;
    }

    result.push(msg);
  });

  return result;
}