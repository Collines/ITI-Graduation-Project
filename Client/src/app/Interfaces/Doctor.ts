import { Gender } from '../Enums/Gender';

export interface Doctor {
  id: number;
  firstName: string;
  lastName: string;
  gender: Gender;
  title: string;
  bio: string;
  departmentId: number;
  departmentTitle: string;
  image: string;
}
