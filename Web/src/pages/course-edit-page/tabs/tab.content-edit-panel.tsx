import { Space, Tooltip, Button, Popconfirm } from 'antd';
import { useTranslation } from 'react-i18next';

import {
  useDeleteCourseMaterialMutation,
  useUpdateCourseMaterialActiveMutation,
} from '../../../api/course-materials.api';
import { DeleteIcon, EditIcon, VisibilityIcon } from '../../../components';
import { CourseMaterial } from '../../../models/course-material.type';
import { CourseMaterialIcon, useMaterialLabels } from '../../../shared';

interface Props {
  material: CourseMaterial;
}

export default function TabContentEditPanel({ material }: Props) {
  const { t } = useTranslation();
  const { getMaterialLabel } = useMaterialLabels();

  const [updateCourseMaterialActive, { isLoading: isUpdateActiveLoading }] =
    useUpdateCourseMaterialActiveMutation();

  const [deleteCourseMaterial] = useDeleteCourseMaterialMutation();

  const handleUpdateCourseMaterialActive = async () => {
    await updateCourseMaterialActive({
      id: material.id,
      isActive: !material.isActive,
    }).unwrap();
  };

  const handleDeleteCourseMaterial = async () => {
    await deleteCourseMaterial({ id: material.id }).unwrap();
  };

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
        <Button
          loading={isUpdateActiveLoading}
          icon={<VisibilityIcon visibility={material.isActive} />}
          onClick={handleUpdateCourseMaterialActive}
        />
      </Tooltip>
      <Tooltip title={t('actions.edit')}>
        <Button icon={<EditIcon />} />
      </Tooltip>
      <Tooltip title={t('actions.delete')}>
        <Popconfirm
          title={t('actions.confirm_deletion')}
          onConfirm={handleDeleteCourseMaterial}
        >
          <Button icon={<DeleteIcon />} />
        </Popconfirm>
      </Tooltip>
    </Space.Compact>
  );
}
