import { CampaignDetail } from './campaignDetail';
import { Employee } from 'src/app/models/employee';
export interface Campaign {
    id?: number,
    user?: Employee,
    startDate?: number,
    ifinishDated?: number,
    isActive?: boolean,
    campaignDetails?: CampaignDetail[]
}