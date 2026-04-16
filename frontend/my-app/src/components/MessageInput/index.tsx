import { View, TextInput, TouchableOpacity, Text } from "react-native";
import { styles } from "./styles";

type Props = {
  message: string;
  setMessage: (text: string) => void;
  onSend: () => void;
};

export function MessageInput({ message, setMessage, onSend }: Props) {
  return (
    <View style={styles.inputContainer}>
      <TextInput
        style={styles.input}
        value={message}
        onChangeText={setMessage}
        placeholder="Digite uma mensagem..."
      />

      <TouchableOpacity style={styles.sendButton} onPress={onSend}>
        <Text>Enviar</Text>
      </TouchableOpacity>
    </View>
  );
}