import { App } from 'antd';
import { ItemType } from 'antd/es/menu/hooks/useItems';
import { useTranslation } from 'react-i18next';
import { useNavigate } from 'react-router-dom';

import { useDeleteUserMutation } from '../../api/users.api';
import useDisplayError from '../../hooks/use-display-error';
import { User } from '../../models/user.interface';
import { DeleteIcon, EditIcon } from '../../shared';

export default function useUsersActions() {
  const { t } = useTranslation();
  const navigate = useNavigate();

  const { modal } = App.useApp();
  const { displayError } = useDisplayError();

  const [deleteUser] = useDeleteUserMutation();

  const getActionItems = (record: User): ItemType[] => [
    {
      key: '1',
      icon: <EditIcon />,
      label: t('actions.edit'),
      onClick: () => navigate(`/users/edit/${record.id}`),
    },
    {
      key: '2',
      icon: <DeleteIcon />,
      label: t('actions.delete'),
      onClick: () =>
        modal.confirm({
          title: t('users_page.confirm_delete_title', { email: record.email }),
          content: t('actions.confirm_delete'),
          okButtonProps: { danger: true },
          onOk: () =>
            deleteUser({ id: record.id })
              .unwrap()
              .catch((error) => displayError(error)),
        }),
    },
  ];

  return { getActionItems };
}
