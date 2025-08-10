export interface IOrder {
    id: string;
    customer: string;
    product: string;
    value: number;
    orderStatus: string;
    createdAt: Date
}