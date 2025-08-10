import { Package } from "lucide-react"

export function Header() {
  return (
    <header className="flex items-center justify-between px-6 py-4 border-b bg-blue-800 text-white dark:bg-blue-700">
      <div className="flex items-center gap-2">
        <Package className="w-6 h-6" />
        <h1 className="text-3xl font-semibold">Gerenciador de pedidos</h1>
      </div>
      <span className="text-sm opacity-90">
        Feito por <strong>Bruno Rafael</strong>
      </span>
    </header>
  )
}
