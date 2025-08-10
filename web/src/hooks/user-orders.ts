import { useMutation, useQuery } from '@tanstack/react-query'
import type { OrderFormValues } from '../components/orders/order-form'
import { ordersApi } from '../services/orders-api'
import { toast } from 'sonner'
import { queryClient } from '../lib/query-client'

interface UpdateFormRequest {
    order: OrderFormValues,
    orderId: string,
}

export const useOrders = () => {
  return useQuery({
    queryKey: ['orders'],
    queryFn: ordersApi.getAll,
  })
}

export const useCreateOrder = () => {
  return useMutation({
    mutationFn: async (data: OrderFormValues) => {
      return ordersApi.create(data)
    },
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['orders'] })
      toast("Pedido criado com sucesso!", {
        richColors: true,
      })
    },
    onError: (error) => {
      toast("Erro ao criar pedido.", {
        description: error.message,
      })
    },
  })
}

export const useUpdateOrder = () => {
  return useMutation({
    mutationFn: async ( { order, orderId } : UpdateFormRequest) => {
      return ordersApi.update(order, orderId)
    },
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['orders'] })
      toast("Sucesso!", {
        description: "Pedido atualizado com sucesso!",
      })
    },
    onError: (error) => {
      console.error('Erro ao atualizar pedido:', error)
      toast("Erro.", {
        description: error.message,
      })
    },
  })
}

export const useDeleteOrder = () => {
  return useMutation({
    mutationFn: ordersApi.delete,
    onSuccess: (_, variables) => {
      queryClient.invalidateQueries({ queryKey: ['orders'] })
      toast("Sucesso!", {
        description: `Pedido ${variables} deletado com sucesso!`,
        richColors: true,
      })
    },
    onError: () => {
      toast("Erro ao deletar pedido.", {
        description: "Não foi possível deletar o pedido.",
      })
    },
  })
}
