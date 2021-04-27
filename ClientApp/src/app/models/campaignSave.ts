import { Employee } from 'src/app/models/employee';
import { Customer } from "./customer";

export interface CampaignSave {
    id?: number,
    userId?: string,
    name?: string,
    startDate?: Date,
    finishDate?: Date,
    isActive?: boolean,
    customers?: Customer[],
    user?: Employee
}