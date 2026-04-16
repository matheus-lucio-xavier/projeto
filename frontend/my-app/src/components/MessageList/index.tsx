import { FlatList, View, Text } from "react-native";
import { styles } from "@/styles/home.styles";
import { MessageBubble } from "../MessageBubble";

type Mensagem = {
  id: string;
  origemId: string;
  type: string;
  content: string;
  createdAt: string;
};

type Props = {
  mensagens: Mensagem[];
};

export function MessageList({ mensagens }: Props) {
  return (
    <FlatList
      data={mensagens}
      keyExtractor={(item) => item.id}
      renderItem={({ item }) => (
        //<View style={styles.message}>
        //  <Text>{item.content}</Text>
        //  <Text>{new Date(item.createdAt).toLocaleTimeString()}</Text>
        //</View>
        <MessageBubble mensagem={item}/>
      )}
    />
  );
}