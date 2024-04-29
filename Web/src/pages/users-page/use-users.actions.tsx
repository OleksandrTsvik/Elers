import { ItemType } from 'antd/es/menu/hooks/useItems';
import { useTranslation } from 'react-i18next';

import { DeleteIcon, EditIcon } from '../../shared';

export default function useUsersActions() {
  const { t } = useTranslation();

  const getActionItems = (): ItemType[] => [
    {
      key: '1',
      icon: <EditIcon />,
      label: t('actions.edit'),
    },
    {
      key: '2',
      icon: <DeleteIcon />,
      label: t('actions.delete'),
    },
  ];

  return { getActionItems };
}
