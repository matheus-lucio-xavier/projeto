import { View, Text, FlatList, TextInput, TouchableOpacity, Alert, KeyboardAvoidingView, Platform, ScrollView } from "react-native";
import { useLocalSearchParams } from "expo-router";
import { useEffect, useState } from "react";
import { styles } from "@/styles/home.styles";
import { getConversaMensagens, postConversaMensagem } from "@/services/conversaService";
import { MessageInput } from "@/components/MessageInput";
import { MessageList } from "@/components/MessageList";

export default function Chat() {
    type Mensagem = {
        id: string;
        origemId: string;
        type: string;
        content: string;
        isMine: boolean;
        createdAt: string;
    };

    const { id, nome } = useLocalSearchParams();
    const [ inputContent, setInputContent] = useState("");
    const [ mensagens, setMensagens] = useState<Mensagem[]>([]);

    const fetchData = async () => {
        try{
            const response = await getConversaMensagens(id as string)
  
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

    const handleSend = async () => {
        try{
            if (!inputContent.trim()) return;

            setInputContent("");

            const mensagem = {
                type: 0,
                content: inputContent
            }

            const response = await postConversaMensagem(id as string, mensagem)
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
    };

    useEffect(() => {
        if (!id) return;

        fetchData();

        const interval = setInterval(fetchData, 3000); // atualiza a cada 3s

        return () => clearInterval(interval);
    }, [id]);

    return (
        <KeyboardAvoidingView
            style={{ flex: 1 }}
            behavior={Platform.OS === "ios" ? "padding" : "height"}
        >
            <View style={styles.container}>
                <Text>/////{nome}/////</Text>

                {/* Scroll só nas mensagens */}
                <MessageList mensagens={mensagens} />

                {/* Input fora do scroll */}
                <MessageInput
                    message={inputContent}
                    setMessage={setInputContent}
                    onSend={handleSend}
                />
            </View>
        </KeyboardAvoidingView>
    );
}