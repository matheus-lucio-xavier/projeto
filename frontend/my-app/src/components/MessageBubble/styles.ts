import { StyleSheet } from "react-native";

export const styles = StyleSheet.create({
  message: {
    padding: 10,
    margin: 8,
    backgroundColor: "#eee",
    borderRadius: 8,
    maxWidth: "80%", // importante pra limitar largura
  },
  isMine: {
    alignSelf: "flex-end",
  },
  isNotMine: {
    alignSelf: "flex-start",
  },

  dataText: {
    fontSize: 10, 
    alignSelf: "flex-end",
    marginLeft: 20,
  }
});