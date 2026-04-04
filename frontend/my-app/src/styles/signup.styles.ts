import { StyleSheet } from "react-native";

export const styles = StyleSheet.create({
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
        alignItems: "center",
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