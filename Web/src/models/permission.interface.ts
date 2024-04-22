export interface Permission {
  id: string;
  name: string;
  description: string;
}

export interface RolePermissions {
  id: string;
  name: string;
  description: string;
  isSelected: boolean;
}
