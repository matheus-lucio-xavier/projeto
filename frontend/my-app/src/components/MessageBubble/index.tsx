import { View, Text } from "react-native";
import { styles } from "./styles";

type Mensagem = {
  id: string;
  origemId: string;
  type: string;
  content: string;
  isMine: boolean;
  createdAt: string;
};

type Props = {
  mensagem: Mensagem;
};

export function MessageBubble({ mensagem }: Props) {
  return (
    <View style={[styles.message, (mensagem.isMine) ? styles.isMine : styles.isNotMine]}>
        <Text>{mensagem.content}</Text>
        <Text style={styles.dataText}>
            {new Date(mensagem.createdAt).toLocaleTimeString("pt-br", {
              hour: "2-digit",
              minute: "2-digit"
            })}
        </Text>
    </View>
  );
}