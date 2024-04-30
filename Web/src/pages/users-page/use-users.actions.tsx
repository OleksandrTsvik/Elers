import { ItemType } from 'antd/es/menu/hooks/useItems';
import { useTranslation } from 'react-i18next';
import { useNavigate } from 'react-router-dom';

import { User } from '../../models/user.interface';
import { DeleteIcon, EditIcon } from '../../shared';

export default function useUsersActions() {
  const { t } = useTranslation();
  const navigate = useNavigate();

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
    },
  ];

  return { getActionItems };
}
