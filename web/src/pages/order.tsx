import { useState } from "react"
import { Dialog, DialogTrigger } from "@radix-ui/react-dialog"
import { Header } from "../components/header"
import { Button } from "../components/ui/button"
import { OrderDialog } from "../components/orders/order-dialog"
import { Edit, PlusCircle, Trash2 } from "lucide-react"
import type { IEntityTableCell } from "../types/entity-table-cell"
import type { IOrder } from "../types/order"
import { EntityTable } from "../components/entity-table"
import type { IHeadCell } from "../types/head-cells"
import { useDeleteOrder, useOrders } from "../hooks/user-orders"
import { format } from "date-fns"
import { getOrderStatusColor } from "../utils/get-order-status-color"

export function Order() {
  const { data: orders, isLoading, error: getOrdersError } = useOrders()
  const { mutateAsync: deleteOrder, isPending: isDeleting } = useDeleteOrder()

  const loading = isLoading || isDeleting

  const headCells: IHeadCell[] = [
    { label: "Identificador" },
    { label: "Cliente" },
    { label: "Produto" },
    { label: "Preço" },
    { label: "Status" },
    { label: "Data de criação" },
    { label: "" },
    { label: "" }
  ]

  const [editingOrder, setEditingOrder] = useState<IOrder | null>(null)
  const [isDialogOpen, setIsDialogOpen] = useState(false)

  const openCreateDialog = () => {
    setEditingOrder(null)
    setIsDialogOpen(true)
  }

  const openEditDialog = (order: IOrder) => {
    setEditingOrder(order)
    setIsDialogOpen(true)
  }

  const createRows = (order: IOrder): IEntityTableCell => {
    return {
      id: order.id,
      elements: [
        { value: order.id },
        { value: order.customer },
        { value: order.product },
        {
          value: new Intl.NumberFormat("pt-BR", {
            style: "currency",
            currency: "BRL"
          }).format(order.value)
        },
        {
          value: (
            <span
              className={`px-2 py-1 rounded-full text-xs font-semibold ${getOrderStatusColor(
                order.orderStatus
              )}`}
            >
              {order.orderStatus}
            </span>
          )
        },
        { value: format(new Date(order.createdAt), "dd/MM/yyyy HH:mm:ss") },
        {
          value: (
            <Button
              variant="ghost"
              size="icon"
              className="text-gray-500 hover:text-gray-700 cursor-pointer"
              onClick={() => openEditDialog(order)}
            >
              <span className="sr-only">Editar</span>
              <Edit className="w-4 h-4" />
            </Button>
          )
        },
        {
          value: (
            <Button
              onClick={async () => await deleteOrder(order.id)}
              variant="ghost"
              size="icon"
              className="text-red-500 hover:text-red-700 cursor-pointer"
            >
              <span className="sr-only">Excluir</span>
              <Trash2 className="w-4 h-4" />
            </Button>
          )
        }
      ]
    } as IEntityTableCell
  }

  const entities: IEntityTableCell[] = orders?.map(createRows) ?? []

  return (
    <div className="flex flex-col space-y-8 px-6 py-4">
      <Header />
      <div className="flex flex-wrap justify-between items-center gap-4">
        <div className="flex flex-col items-start gap-1">
          <h2 className="text-2xl font-semibold tracking-tight text-gray-900 dark:text-gray-100">
            Lista de Pedidos
          </h2>
          <p className="text-sm text-gray-500 dark:text-gray-400">
            Acompanhe o status dos pedidos mais recentes
          </p>
        </div>
        <Dialog open={isDialogOpen} onOpenChange={setIsDialogOpen}>
          <DialogTrigger asChild>
            <Button
              className="gap-2 shadow-sm hover:shadow-md transition-shadow bg-blue-800 hover:bg-blue-400"
              onClick={openCreateDialog}
            >
              <PlusCircle className="w-4 h-4" /> Novo Pedido
            </Button>
          </DialogTrigger>
          <OrderDialog order={editingOrder} />
        </Dialog>
      </div>

      <div className="rounded-lg border border-gray-200 dark:border-gray-800 shadow-sm bg-white dark:bg-gray-900">
        <EntityTable entities={entities} headCells={headCells} loading={loading} error={getOrdersError} />
      </div>
    </div>
  )
}
