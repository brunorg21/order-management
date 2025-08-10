

export const getOrderStatusColor = (status: string): string => {
    switch (status) {
        case "Pendente":
            return 'bg-yellow-500 text-white';
        case "Processando":
            return 'bg-blue-500 text-white';
        case "Finalizado":
            return 'bg-green-500 text-white';
        default:
            return 'bg-gray-500 text-white';
    }
}