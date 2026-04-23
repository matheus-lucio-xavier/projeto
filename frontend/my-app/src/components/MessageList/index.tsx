import { FlatList, View, Text } from "react-native";
import { styles } from "./styles";
import { MessageBubble } from "../MessageBubble";
import { KeyboardAwareFlatList } from "react-native-keyboard-aware-scroll-view";
import { groupMessagesByDate } from "@/utils/groupMessages";

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

type Props = {
  mensagens: Mensagem[];
};

export function MessageList({ mensagens = [] }: Props) {
  console.log(mensagens)
  const data = groupMessagesByDate(mensagens ?? [])

  return (
    <FlatList
      data={data}
      keyExtractor={(item) => item.id}
      renderItem={({ item }) => {
        if (item.type === "date"){
          const dateItem = item as DateItem
          return (
            <View style={styles.dateContainer}>
              <Text>{dateItem.date}</Text>
            </View>
          );
        }

        const mensagemItem = item as Mensagem
        return <MessageBubble mensagem={mensagemItem}/>
      }}
    />
  );
}