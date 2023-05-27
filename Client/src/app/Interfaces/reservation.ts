import { Doctor } from './Doctor';
import { ReservationStatus } from '../Enums/ReservationStatus';

export interface Reservation {
  id: number;
  dateTime: string;
  queue: number;
  status: ReservationStatus;
  doctor: Doctor | null;
  doctorId: number;
  patientId: number;
}
