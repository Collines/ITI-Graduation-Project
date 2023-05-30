import { Gender } from 'src/app/Enums/Gender';
import { PatientImage } from './PatientImage';

export interface UserUpdate {
  firstName_EN: string;
  ssn: string;
  firstName_AR: string;
  lastName_EN: String;
  lastName_AR: string;
  email: string;
  phone: string;
  dob: string;
  medicalHistory: string | null;
  password: string | null;
  image: PatientImage;
  gender: Gender;
}
