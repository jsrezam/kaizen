import { Category } from './category';
export interface Product {
    id?: number
    category: Category
    name?: string
    quantityPerUnit?: number
    unitPrice?: number
    unitsInStock?: number
    isDiscontinued?: boolean
}