import { View, Text, FlatList, TextInput, TouchableOpacity, Alert } from "react-native";
import { useLocalSearchParams } from "expo-router";
import { useEffect, useState } from "react";
import { styles } from "@/styles/home.styles";
import { getConversaMensagens } from "@/services/conversaService";
import { MessageInput } from "@/components/MessageInput";
import { MessageList } from "@/components/MessageList";

export default function Chat() {
    type Mensagem = {
        id: string;
        origemId: string;
        type: string;
        content: string;
        createdAt: string;
    };

    const { id, nome } = useLocalSearchParams();
    const [message, setMessage] = useState("");
    const [ mensagens, setMensagens] = useState<Mensagem[]>([]);

    const fetchData = async () => {
        try{
            const response = await getConversaMensagens(id as string)
  
            console.log(response.data)
            setMensagens(response.data)
        }catch (error: any) {
            if (error.response) {
                // erro vindo da API (400, 401, etc)
                console.log("Erro da API:", error.response.data)
  
                Alert.alert("Erro", JSON.stringify(error.response.data))
            } else {
                // erro de rede
                console.log("Erro geral:", error)
                Alert.alert("Erro de conexão")
            }
        }
    }

    const handleSend = () => {
        if (!message.trim()) return;

        console.log("Enviar mensagem:", message);
        setMessage("");
    };

    useEffect(() => {
        if (!id) return;

        fetchData();

        const interval = setInterval(fetchData, 3000); // atualiza a cada 3s

        return () => clearInterval(interval);
    }, [id]);

    return (
        <View style={styles.container}>
            <Text>/////{nome}/////</Text>

            <MessageList mensagens={mensagens} />

            <MessageInput
                message={message}
                setMessage={setMessage}
                onSend={handleSend}
            />
        </View>
    );
}