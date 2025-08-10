import type { IOrder } from "../../types/order";
import {
  DialogContent,
  DialogDescription,
  DialogHeader,
  DialogTitle,
} from "../ui/dialog"
import { OrderForm } from "./order-form"

interface OrderDialogProps {
  order: IOrder | null;
}

export function OrderDialog({ order }: OrderDialogProps) {
  const isEditing = !!order

  return (
        <DialogContent className="sm:max-w-[425px]">
          <DialogHeader>
            <DialogTitle>{isEditing ? `Editar pedido ${order.id}` : "Criar novo pedido"}</DialogTitle>
            <DialogDescription>
                Preencha os detalhes do pedido abaixo para {isEditing ? "atualizar" : "criar um registro"}.
            </DialogDescription>
          </DialogHeader>
          <OrderForm order={order}/>
        </DialogContent>
  )
}
