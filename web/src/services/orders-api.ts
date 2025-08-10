import type {  OrderFormValues } from "../components/orders/order-form"
import type { IOrder } from "../types/order"


export const API_BASE_URL = import.meta.env.VITE_API_URL

export const ordersApi = {
  getAll: async (): Promise<IOrder[]> => {
    const response = await fetch(`${API_BASE_URL}/api/orders`)

    if (!response.ok) {
      throw new Error('Failed to fetch orders')
    }
    const result = await response.json()

    return result.orders as IOrder[]
  },

  create: async (order: OrderFormValues): Promise<IOrder> => {
    const response = await fetch(`${API_BASE_URL}/api/orders`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(order),
    })
    if (!response.ok) {
      throw new Error('Failed to create order')
    }
    return response.json()
  },

  update: async (order: OrderFormValues, orderId: string): Promise<IOrder> => {
    const response = await fetch(`${API_BASE_URL}/api/orders/${orderId}`, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(order),
    })
    if (!response.ok) {
      throw new Error('Failed to update order')
    }
    return response.json()
  },

  delete: async (id: string): Promise<void> => {
    const response = await fetch(`${API_BASE_URL}/api/orders/${id}`, {
      method: 'DELETE',
    })
    if (!response.ok) {
      throw new Error('Failed to delete order')
    }
  },
}
