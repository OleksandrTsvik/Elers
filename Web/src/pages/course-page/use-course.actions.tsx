import { ItemType } from 'antd/es/menu/hooks/useItems';
import { useTranslation } from 'react-i18next';
import { useNavigate } from 'react-router-dom';

export default function useCourseActions() {
  const { t } = useTranslation();
  const navigate = useNavigate();

  const getActionItems = (courseId: string): ItemType[] => [
    {
      key: '1',
      label: t('actions.edit'),
      onClick: () => navigate(`/courses/edit/${courseId}`),
    },
  ];

  return { getActionItems };
}
