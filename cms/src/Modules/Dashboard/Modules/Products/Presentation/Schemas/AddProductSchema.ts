import { z } from "zod";

export const AddProductSchema = z.object({
  name: z
    .string()
    .min(5, { error: "Debe tener al menos 5 caracteres" })
    .max(100, { error: "Debe tener máximo 100 caracteres" }),
  price: z.number().min(0.1, { error: "El precio debe ser mayor que cero" }),

  quantity: z
    .number()
    .int()
    .min(1, { error: "Debe tener al menos una unidad en existencias" }),

  image: z.string().nonempty("Debe agregar la url de la imagen"),
  category: z
    .number()
    .int()
    .min(1, { error: "Debe seleccionar una categoría" }),
  description: z
    .string()
    .min(10, { error: "Debe tener al menos 10 caracteres" })
    .max(400, { error: "Debe tener máximo 400 caracteres" }),
});

export type AddProductFormData = z.infer<typeof AddProductSchema>;
