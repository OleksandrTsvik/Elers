export interface Permission {
  id: string;
  name: string;
  description: string;
}

export interface RolePermissionListItem {
  id: string;
  name: string;
  description: string;
  isSelected: boolean;
}
