import type { ReactNode } from "react"

interface ICellValue {
    value: string | ReactNode
}

export interface IEntityTableCell {
    id: string;
    elements: ICellValue[]
}