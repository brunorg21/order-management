import { useForm } from "react-hook-form";
import { Form, FormControl, FormDescription, FormField, FormItem, FormLabel, FormMessage } from "../ui/form";
import { Button } from "../ui/button";
import { Input } from "../ui/input";
import { zodResolver } from "@hookform/resolvers/zod";
import { z } from "zod";
import { useCreateOrder, useUpdateOrder } from "../../hooks/user-orders";
import type { IOrder } from "../../types/order";
import { Loader } from "lucide-react";

const createOrderSchema = z.object({
  customer: z.string().min(3, {
    message: "Cliente deve ter no mínimo 3 caracteres.",
  }),
  product: z.string(),
  value: z.string()
    .min(1, { message: "Valor é obrigatório." })
    .regex(/^\d+(\.\d{1,2})?$/, {
      message: "Valor deve ser um número válido com até 2 casas decimais.",
    })
    .refine((val) => parseFloat(val) > 0, {
      message: "Valor deve ser maior que zero.",
    }),
})

export type OrderFormValues = z.infer<typeof createOrderSchema>;

interface OrderFormProps {
    order: IOrder | null;
}

export function OrderForm({ order }: OrderFormProps) {
    const { mutateAsync: createOrder, isPending: isCreating } = useCreateOrder()
    const { mutateAsync: updateOrder, isPending: isUpdating } = useUpdateOrder()

    const loading = isCreating || isUpdating

    const form = useForm<OrderFormValues>({
        resolver: zodResolver(createOrderSchema),
        defaultValues: {
            customer: order?.customer ?? "",
            product: order?.product ?? "",
            value: order ? String(order.value) : "",
        },
    })
    
    async function onSubmit(values: OrderFormValues) {
        if(order) {
            await updateOrder({ order: values, orderId: order.id })
        } else {
            await createOrder(values)
        }
    }

    const labelButton = order ? "Atualizar pedido" : "Cadastrar pedido"

    return (
        <Form {...form}>
            <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-2">
                <FormField
                    control={form.control}
                    name="customer"
                    render={({ field }) => (
                        <FormItem>
                            <FormLabel>Cliente</FormLabel>
                            <FormControl>
                                <Input placeholder="Cliente" {...field} />
                            </FormControl>
                            <FormDescription>
                                Nome do cliente que está realizando o pedido.
                            </FormDescription>
                            <FormMessage />
                        </FormItem>
                    )}
                />
                <FormField
                    control={form.control}
                    name="product"
                    render={({ field }) => (
                        <FormItem>
                            <FormLabel>Produto</FormLabel>
                            <FormControl>
                                <Input placeholder="Produto" {...field} />
                            </FormControl>
                            <FormDescription>
                                Nome do produto que está sendo pedido.
                            </FormDescription>
                            <FormMessage />
                        </FormItem>
                    )}
                />
                <FormField
                    control={form.control}
                    name="value"
                    render={({ field }) => (
                        <FormItem>
                            <FormLabel>Preço</FormLabel>
                            <FormControl>
                                <Input type="number" placeholder="Preço" {...field} />
                            </FormControl>
                            <FormDescription>
                                Preço do produto.
                            </FormDescription>
                            <FormMessage />
                        </FormItem>
                    )}
                />  
                <Button disabled={loading} className="bg-blue-800 hover:bg-blue-400" type="submit">
                    {loading ? <Loader className="mr-2 h-4 w-4 animate-spin" /> : null}
                    {labelButton}
                </Button>
            </form>
        </Form>
    )
}


