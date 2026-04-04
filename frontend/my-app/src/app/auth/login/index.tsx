import { Input } from "@/components/input"
import { Button } from "@/components/button"
import { View, Text, ScrollView, KeyboardAvoidingView, Platform, Alert} from "react-native"
import { useState } from "react"
import { Link, router, Router } from "expo-router"
import { styles } from "../../../styles/login.styles"
import { login } from "@/services/authService"

export default function Login(){
    const [email, setEmail] = useState("")
    const [password, setPassword] = useState("")


    const handleLogIn = async () => {

        try{
            const response = await login(email, password)

            Alert.alert(`Login efetuado com ${response.data.usuario.nome}`)
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


    return (
        <KeyboardAvoidingView style={{flex: 1}} behavior={Platform.select({ ios: "padding", android: "height"})}>
            <ScrollView contentContainerStyle={{flexGrow: 1}} keyboardShouldPersistTaps="handled" showsVerticalScrollIndicator={false}>
                <View style={styles.container}>

                    <Text style={styles.title}>Entrar</Text>
                    <Text style={styles.subtitle}>accese sua conta com e-mail e senha</Text>
                    
                    <View style={styles.form}>
                        <Input
                            type="text"
                            icon="person"
                            placeholder="E-mail"
                            onChangeText={setEmail}
                            keyboardType="email-address"
                        />
                        <Input
                            type="password"
                            icon="pencil"
                            placeholder="Senha"
                            onChangeText={setPassword}
                        />
                        <Button label="entrar" onPress={handleLogIn}/>
                    </View>

                    <Text style={styles.footerText}>
                        Não tem uma conta? <Link style={styles.footerLink} href={"/auth/signup"}>cadastre-se!</Link>
                    </Text>

                </View>
            </ScrollView>
        </KeyboardAvoidingView>
    )
}