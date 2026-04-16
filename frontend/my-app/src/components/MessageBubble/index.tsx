import { View, Text } from "react-native";
import { styles } from "./styles";

type Mensagem = {
  id: string;
  origemId: string;
  type: string;
  content: string;
  createdAt: string;
};

type Props = {
  mensagem: Mensagem;
};

export function MessageBubble({ mensagem }: Props) {
  return (
    <View style={styles.message}>
        <Text>{mensagem.content}</Text>
        <Text style={{ fontSize: 10 }}>
            {new Date(mensagem.createdAt).toLocaleTimeString()}
        </Text>
    </View>
  );
}