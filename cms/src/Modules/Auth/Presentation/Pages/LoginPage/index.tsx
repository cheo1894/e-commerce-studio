import Button from "@/Shared/Components/Button";
import ButtonLinks from "@/Shared/Components/ButtonLinks";
import TextInput from "@/Shared/Components/TextInput";
import useLoginForm from "../../hooks/useLoginForm";

type Props = {};

function Index({}: Props) {
  const {
    handleSubmit,
    register,
    onSubmit,
    errors,
    isSubmitting,
    isSubmitted,
    reset,
  } = useLoginForm();

  return (
    <>
      <form className="Form-body" onSubmit={handleSubmit(onSubmit)}>
        <TextInput Title="User" PlaceHolder="Ej: User" {...register("user")} />

        {errors.user && (
          <>
            <br />
            <span style={{ color: "red" }}>{errors.user.message}</span>
          </>
        )}
        <TextInput
          type="password"
          Title="Password"
          PlaceHolder="*******"
          {...register("password")}
        />

        {errors.password ? (
          <>
            <br />
            <span style={{ color: "red" }}>{errors.password.message}</span>
            <br />
            <br />
          </>
        ) : errors.root ? (
          <>
            <br />
            <span style={{ color: "red" }}>{errors.root.message}</span>
            <br />
            <br />
          </>
        ) : (
          <br />
        )}

        <Button
          Title={isSubmitting ? "Cargando..." : "Login"}
          Type="submit"
          disabled={isSubmitting}
        />
      </form>
    </>
  );
}

export default Index;
