import Button from "@/Shared/Components/Button";
import ButtonLinks from "@/Shared/Components/ButtonLinks";
import TextInput from "@/Shared/Components/TextInput";
import React, { useState } from "react";
import { useNavigate } from "react-router";

type Props = {};

function Index({}: Props) {
  const navigate = useNavigate();
  const [user, setUser] = useState("");
  const [password, setPassword] = useState("");
  return (
    <>
      <h3>Login</h3>
      <TextInput
        value={user}
        onTextChange={setUser}
        Title="User"
        PlaceHolder="Ej: User"
      />
      <TextInput
        value={password}
        onTextChange={setPassword}
        type="password"
        Title="Password"
        PlaceHolder="*******"
      />
      <TextInput
        value={password}
        onTextChange={setPassword}
        type="password"
        Title="Repeat Password"
        PlaceHolder="*******"
      />
      <br />
      <br />
      <Button
        Title="Login"
        onClick={function (): void {
          console.log("ejecutando bottón");
        }}
      />
      <ButtonLinks
        text="Login"
        onClick={() => {
          navigate("/auth/login");
        }}
      />
    </>
  );
}

export default Index;
