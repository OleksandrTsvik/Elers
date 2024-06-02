import { Button } from 'antd';
import { useTranslation } from 'react-i18next';

import { CourseRolesModalMode } from './course-roles.modal-mode.enum';
import { classnames } from '../../utils/helpers';

interface Props {
  isLoading: boolean;
  updateModalMode: (modalMode: CourseRolesModalMode) => void;
}

export default function CourseRoleCreationButton({
  isLoading,
  updateModalMode,
}: Props) {
  const { t } = useTranslation();

  return (
    <Button
      className={classnames('right-btn', 'mb-field')}
      type="primary"
      loading={isLoading}
      onClick={() => updateModalMode(CourseRolesModalMode.Create)}
    >
      {t('course_roles_page.create_role')}
    </Button>
  );
}
