import { UserRole } from '../../models/role.interface';
import { User } from '../../models/user.interface';
import { UserFormValues } from '../../shared';

export function getInitialValues(
  user: User,
  listRoles?: UserRole[],
): UserFormValues {
  const initialValues: UserFormValues = {
    password: '',
    roleIds: [],
    ...user,
  };

  if (listRoles) {
    initialValues.roleIds = listRoles
      .filter((item) => user.roles.includes(item.name))
      .map((item) => item.id);
  }

  return initialValues;
}
