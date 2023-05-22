import { Gender } from 'src/app/Enums/Gender';

export interface UserEdit {
  ssn: string;
  gender: Gender;
  firstName_EN: string;
  firstName_AR: string;
  lastName_EN: String;
  lastName_AR: string;
  email: string;
  phone: string;
  dob: Date;
  medicalHistory: string | null;
  password: string | null;
}
