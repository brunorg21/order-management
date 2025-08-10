import { Toaster } from "sonner";
import { queryClient } from "./lib/query-client";
import { Order } from "./pages/order";
import { QueryClientProvider } from "@tanstack/react-query";
import { SignalRProvider } from "./contexts/signalr-context";

export function App() {
  return (
    <QueryClientProvider client={queryClient}>
      <SignalRProvider>
          
            <div className="h-screen">
              <Order/>
            </div>
            <Toaster/>
          
      </SignalRProvider>
    </QueryClientProvider>
  )
}


