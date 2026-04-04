import { Input } from "@/components/input"
import { Button } from "@/components/button"
import { View, Text, ScrollView, KeyboardAvoidingView, Platform} from "react-native"
import { Link } from "expo-router"
import { styles } from "../../../styles/signup.styles"

export default function Signup(){
    return (
        <KeyboardAvoidingView style={{flex: 1}} behavior={Platform.select({ ios: "padding", android: "height"})}>
            <ScrollView contentContainerStyle={{flexGrow: 1}} keyboardShouldPersistTaps="handled" showsVerticalScrollIndicator={false}>
                <View style={styles.container}>

                    <Text style={styles.title}>Registrar</Text>
                    <Text style={styles.subtitle}>cadastre sua conta com e-mail e senha</Text>
                    
                    <View style={styles.form}>
                        <Input
                            type="text"
                            icon="person"
                            placeholder="Nome"
                        />
                        <Input
                            type="text"
                            icon="person"
                            placeholder="E-mail"
                        />
                        <Input
                            type="password"
                            icon="pencil"
                            placeholder="Senha"
                        />
                        <Input
                            type="password"
                            icon="pencil"
                            placeholder="Confirmar senha"
                        />
                        <Button label="cadastrar"/>
                    </View>

                    <Text style={styles.footerText}>
                        Ja possui uma conta? <Link style={styles.footerLink} href="/auth/login">login!</Link>
                    </Text>

                </View>
            </ScrollView>
        </KeyboardAvoidingView>
    )
}