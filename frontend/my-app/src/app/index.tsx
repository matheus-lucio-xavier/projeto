import { Input } from "@/components/Input"
import { Button } from "@/components/Button"
import { StyleSheet, View, Text, ScrollView, KeyboardAvoidingView, Platform, Alert} from "react-native"
import { useState } from "react"
import { Link } from "expo-router"

export default function Index(){
    const [email, setEmail] = useState("")
    const [password, setPassword] = useState("")


    function handleSignIn(){
        if (!email.trim() || !password.trim()){
            return Alert.alert("Entrar", "Preencha Email e Senha para entrar.")
        }

        Alert.alert("Bem-vindo", `Login com ${email}`)
    }


    return (
        <KeyboardAvoidingView style={{flex: 1}} behavior={Platform.select({ ios: "padding", android: "height"})}>
            <ScrollView contentContainerStyle={{flexGrow: 1}} keyboardShouldPersistTaps="handled" showsVerticalScrollIndicator={false}>
                <View style={styles.container}>

                    <Text style={styles.title}>Entrar</Text>
                    <Text style={styles.subtitle}>accese sua conta com e-mail e senha</Text>
                    
                    <View style={styles.form}>
                        <Input placeholder="E-mail" keyboardType="email-address" onChangeText={setEmail}/>
                        <Input placeholder="Senha" secureTextEntry onChangeText={setPassword}/>
                        <Button label="entrar" onPress={handleSignIn}/>
                    </View>

                    <Text style={styles.footerText}>
                        Não tem uma conta? <Link style={styles.footerLink} href="/signup">cadastre-se!</Link>
                    </Text>

                </View>
            </ScrollView>
        </KeyboardAvoidingView>
    )
}

const styles = StyleSheet.create({
    container: {
        flex: 1, 
        backgroundColor: "#fdfdfd",
        alignItems: "center",
        padding: 64,
        paddingTop: "50%"
    },
    title: {
        fontSize: 32, 
        fontWeight: 900,
    },
    subtitle:{

    },
    illustration: {
        width: "100%",
        height: 330,
        resizeMode: "contain",
        marginTop: 62
    },
    form: {
        width: "100%",
        marginTop: 24,
        gap: 16,
    },
    scroll: {
        flexGrow: 1,
    },
    footerText: {
        textAlign: "center",
        marginTop: 24,
        color: "#585860",
    },
    footerLink: {
        color: "#032ad7",
        fontWeight: 700,
    },
})