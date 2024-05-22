import { Space, Tooltip, Button } from 'antd';
import { useTranslation } from 'react-i18next';

import { DeleteIcon, EditIcon, VisibilityIcon } from '../../../components';
import { CourseMaterial } from '../../../models/course-material.type';
import { CourseMaterialIcon, useMaterialLabels } from '../../../shared';

interface Props {
  material: CourseMaterial;
}

export default function TabContentEditPanel({ material }: Props) {
  const { t } = useTranslation();
  const { getMaterialLabel } = useMaterialLabels();

  return (
    <Space.Compact block size="small">
      <Tooltip title={getMaterialLabel(material.type)}>
        <Button icon={<CourseMaterialIcon type={material.type} />} disabled />
      </Tooltip>
      <Tooltip
        title={
          material.isActive
            ? t('course_edit_page.visible_material')
            : t('course_edit_page.invisible_material')
        }
      >
        <Button icon={<VisibilityIcon visibility={material.isActive} />} />
      </Tooltip>
      <Tooltip title={t('actions.edit')}>
        <Button icon={<EditIcon />} />
      </Tooltip>
      <Tooltip title={t('actions.delete')}>
        <Button icon={<DeleteIcon />} />
      </Tooltip>
    </Space.Compact>
  );
}
