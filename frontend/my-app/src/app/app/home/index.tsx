import { getUserConversas, getUsers } from "@/services/userService";
import { useEffect, useState } from "react";
import { View, Text, Alert, FlatList, TouchableOpacity } from "react-native";
import { Button } from "@/components/button";
import { styles } from "@/styles/home.styles";
import AsyncStorage from "@react-native-async-storage/async-storage";
import { router } from "expo-router";
import { ConversationList } from "@/components/conversationList";

export default function Home(){

    const [conversas, setConversas] = useState([])

    const fetchData = async () => {
        try{
            const response = await getUserConversas()

            console.log(response.data)
            setConversas(response.data)
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

    const handleLogout = async () => {

        try{
            await AsyncStorage.removeItem("token");
            Alert.alert("Logout efetuado")
            router.replace("/")
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

    useEffect(() => {
        fetchData()

        const interval = setInterval(fetchData, 3000); // atualiza a cada 3s

        return () => clearInterval(interval);
    }, [])

    const handleConversationList = (id: string, nome: string) =>
            router.push({
              pathname: "/app/home/chat/[id]", // ajusta conforme sua estrutura
              params: { 
                    id: id,
                    nome: nome
                },
            })

    return (
        <View style={{ flex: 1 }}>
            <ConversationList conversas={conversas} onPressChat={handleConversationList}/>
        </View>
    );

}