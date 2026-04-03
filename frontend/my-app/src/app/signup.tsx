import { Input } from "@/components/Input"
import { Button } from "@/components/Button"
import { StyleSheet, View, Text, ScrollView, KeyboardAvoidingView, Platform} from "react-native"
import { Link } from "expo-router"

export default function Index(){
    return (
        <KeyboardAvoidingView style={{flex: 1}} behavior={Platform.select({ ios: "padding", android: "height"})}>
            <ScrollView contentContainerStyle={{flexGrow: 1}} keyboardShouldPersistTaps="handled" showsVerticalScrollIndicator={false}>
                <View style={styles.container}>

                    <Text style={styles.title}>Registrar</Text>
                    <Text style={styles.subtitle}>cadastre sua conta com e-mail e senha</Text>
                    
                    <View style={styles.form}>
                        <Input placeholder="Nome"/>
                        <Input placeholder="E-mail" keyboardType="email-address"/>
                        <Input placeholder="Senha" secureTextEntry/>
                        <Input placeholder="Confirmar senha" secureTextEntry/>
                        <Button label="cadastrar"/>
                    </View>

                    <Text style={styles.footerText}>
                        Ja possui uma conta? <Link style={styles.footerLink} href="/">login!</Link>
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