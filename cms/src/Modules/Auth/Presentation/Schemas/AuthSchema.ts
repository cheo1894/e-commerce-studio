import { z } from "zod";

export const AuthSchema = z.object({
  user: z.string().min(2, { error: "Debe tener al menos 2 caracteres" }),
  password: z.string().min(5, { error: "Debe tener al menos 5 caracteres" }),
});

export type AuthFormData = z.infer<typeof AuthSchema>;
