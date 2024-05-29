import { App } from 'antd';
import { ItemType } from 'antd/es/menu/hooks/useItems';
import { useTranslation } from 'react-i18next';

import { useDeleteRoleMutation } from '../../api/roles.api';
import { EditIcon, DeleteIcon } from '../../components';
import useDisplayError from '../../hooks/use-display-error';
import useNavigateFrom from '../../hooks/use-navigate-from';
import { RoleListItem } from '../../models/role.interface';

export default function useRolesActions() {
  const { t } = useTranslation();
  const navigateFrom = useNavigateFrom();

  const { modal } = App.useApp();
  const { displayError } = useDisplayError();

  const [deleteRole] = useDeleteRoleMutation();

  const getActionItems = (record: RoleListItem): ItemType[] => [
    {
      key: '1',
      icon: <EditIcon />,
      label: t('actions.edit'),
      onClick: () => navigateFrom(`/roles/edit/${record.id}`),
    },
    {
      key: '2',
      icon: <DeleteIcon />,
      label: t('actions.delete'),
      onClick: () =>
        modal.confirm({
          title: t('roles_page.confirm_delete_title', { role: record.name }),
          content: t('actions.confirm_delete'),
          okButtonProps: { danger: true },
          onOk: () =>
            deleteRole({ id: record.id })
              .unwrap()
              .catch((error) => displayError(error)),
        }),
    },
  ];

  return { getActionItems };
}
