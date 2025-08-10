import { createContext, useContext, useEffect, type ReactNode } from "react"
import { HubConnectionBuilder, LogLevel } from "@microsoft/signalr"
import { toast } from "sonner"
import type { IUpdateOrderStatusEvent } from "../types/update-order-status-event"
import { queryClient } from "../lib/query-client"
import { API_BASE_URL } from "../services/orders-api"

const SignalRContext = createContext<null>(null)

interface SignalRProviderProps {
  children: ReactNode
}

export function SignalRProvider({ children }: SignalRProviderProps) {
  useEffect(() => {
    const conn = new HubConnectionBuilder()
      .withUrl(`${API_BASE_URL}/orderStatus`)
      .configureLogging(LogLevel.Information)
      .build()

    conn.on("UpdateOrderStatus", (data: IUpdateOrderStatusEvent) => {
        queryClient.invalidateQueries({
            queryKey: ["orders"]
        })
        toast.success("Atualização recebida!", {
            description: `Pedido ${data.orderId} atualizado para o status "${data.newStatus}".`,
            richColors: true,
            style: {
                backgroundColor: "#30D15F",
                fontWeight: "bold",
                color:  "black"
            }
        })
    })

    conn.start()
      .then(() => {
        console.log("SignalR conectado")
      })
      .catch(err => console.error("Erro ao conectar SignalR:", err))

    return () => {
      conn.stop()
    }
  }, [])

  return (
    <SignalRContext.Provider value={null}>
      {children}
    </SignalRContext.Provider>
  )
}

// eslint-disable-next-line react-refresh/only-export-components
export const useSignalRContext = () => {
  const ctx = useContext(SignalRContext)
  return ctx
}
