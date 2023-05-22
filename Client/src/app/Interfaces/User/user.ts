export interface User {
  patient: { id: number; fullName: string };
  accessToken: string;
  refreshToken: string;
  expiration: number;
}
