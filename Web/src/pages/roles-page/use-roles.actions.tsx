import { blue, red } from '@ant-design/colors';
import { DeleteFilled, EditFilled } from '@ant-design/icons';
import { App } from 'antd';
import { ItemType } from 'antd/es/menu/hooks/useItems';
import { useTranslation } from 'react-i18next';
import { useNavigate } from 'react-router-dom';

import { useDeleteRoleMutation } from '../../api/roles.api';
import useDisplayError from '../../hooks/use-display-error';
import { ListRoleItem } from '../../models/role.interface';

export default function useRolesActions() {
  const { t } = useTranslation();
  const navigate = useNavigate();

  const { modal } = App.useApp();
  const { displayError } = useDisplayError();

  const [deleteRole] = useDeleteRoleMutation();

  const getActionItems = (record: ListRoleItem): ItemType[] => [
    {
      key: '1',
      icon: <EditFilled style={{ color: blue.primary }} />,
      label: t('actions.edit'),
      onClick: () => navigate(`/roles/edit/${record.id}`),
    },
    {
      key: '2',
      icon: <DeleteFilled style={{ color: red.primary }} />,
      label: t('actions.delete'),
      onClick: () =>
        modal.confirm({
          title: t('roles_page.confirm_delete.title', { role: record.name }),
          content: t('roles_page.confirm_delete.content'),
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
