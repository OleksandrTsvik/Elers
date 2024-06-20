import { UserType } from './user.interface';

export interface CourseMember {
  id: string;
  userId: string;
  userType?: UserType;
  firstName: string;
  lastName: string;
  patronymic: string;
  avatarUrl?: string;
  courseRole?: {
    id?: string;
    description?: string;
  };
}
