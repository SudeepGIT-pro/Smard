/**
 * Master Persistence Engine for SmarD
 * Handles asynchronous Save, Update, Delete, and automatic Load for all 18 Masters.
 */
(function () {
    // Resolve current master controller name from window or current path
    function getControllerName() {
        if (window.masterControllerName) return window.masterControllerName;
        var path = window.location.pathname.replace(/^\/+|\/+$/g, '');
        var segs = path.split('/');
        return segs[0] || 'WallPanelSystem';
    }

    // Get human-friendly master display name (e.g., 'Wall Finishes', 'Wall Panel System', 'Storage Cabinet')
    function getMasterDisplayName() {
        var titleEl = document.querySelector('.wps-header-title');
        if (titleEl && titleEl.textContent.trim()) {
            var txt = titleEl.textContent.trim();
            if (txt === 'Wall System') return 'Wall Panel System';
            return txt;
        }
        var map = {
            'WallPanelSystem': 'Wall Panel System',
            'StorageCabinet': 'Storage Cabinet',
            'WorkStation': 'Work Station',
            'Windows': 'Windows',
            'Sink': 'Sink',
            'ReturnAirSystem': 'Return Air System',
            'PeripheralLight': 'Peripheral Light',
            'LooseFurnitures': 'Loose Furnitures',
            'LGSPanelSystem': 'LGS Panel System',
            'LaminarFlow': 'Laminar Flow',
            'JobWorks': 'Job Works',
            'Doors': 'Doors',
            'FixedFurnitures': 'Fixed Furnitures',
            'Counters': 'Counters',
            'CSSDAFurnitures': 'CSSD Furnitures',
            'BHU': 'BHU',
            'CeilingSystem': 'Ceiling System',
            'AVSU': 'AVSU'
        };
        var ctrl = getControllerName();
        if (map[ctrl]) return map[ctrl];
        return ctrl.replace(/([A-Z])/g, ' $1').trim();
    }
    window.getMasterDisplayName = getMasterDisplayName;

    // Rich Action Toasts (Created, Updated, Deleted) matching smardhomes.algorytham.com design
    function showActionToast(action, opts) {
        opts = opts || {};
        var masterName = opts.masterName || getMasterDisplayName();
        var t = opts.typeIndex || '1';
        var typeName = opts.typeName ? String(opts.typeName).trim() : '';
        if (typeName === ('TYPE ' + t)) typeName = '';

        var subjectHtml = 'TYPE ' + t + (typeName ? ' &ldquo;' + escapeHtml(typeName) + '&rdquo;' : '');

        var title = '';
        var msgHtml = '';
        var bg = '';
        var bigIcon = '';
        var smallIcon = '';

        if (action === 'created' || action === 'create') {
            bg = '#059669';
            title = 'Created — ' + escapeHtml(masterName);
            msgHtml = '<strong>' + subjectHtml + '</strong> has been <strong>created</strong> successfully in <strong>' + escapeHtml(masterName) + '</strong>.';
            bigIcon = '<svg viewBox="0 0 24 24" width="18" height="18" fill="none" stroke="#ffffff" stroke-width="3" stroke-linecap="round" stroke-linejoin="round"><polyline points="20 6 9 17 4 12"/></svg>';
            smallIcon = '<svg viewBox="0 0 24 24" width="14" height="14" fill="none" stroke="#ffffff" stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round"><path d="M22 11.08V12a10 10 0 1 1-5.93-9.14"/><polyline points="22 4 12 14.01 9 11.01"/></svg>';
        } else if (action === 'updated' || action === 'update') {
            bg = '#1D4ED8';
            title = 'Updated — ' + escapeHtml(masterName);
            msgHtml = '<strong>' + subjectHtml + '</strong> has been <strong>updated</strong> successfully in <strong>' + escapeHtml(masterName) + '</strong>.';
            bigIcon = '<svg viewBox="0 0 24 24" width="18" height="18" fill="none" stroke="#ffffff" stroke-width="2.6" stroke-linecap="round" stroke-linejoin="round"><circle cx="12" cy="12" r="10"/><line x1="12" y1="16" x2="12" y2="12"/><line x1="12" y1="8" x2="12.01" y2="8"/></svg>';
            smallIcon = '<svg viewBox="0 0 24 24" width="14" height="14" fill="none" stroke="#ffffff" stroke-width="2.2" stroke-linecap="round" stroke-linejoin="round"><path d="M12 20h9"/><path d="M16.5 3.5a2.121 2.121 0 0 1 3 3L7 19l-4 1 1-4L16.5 3.5z"/></svg>';
        } else if (action === 'deleted' || action === 'delete') {
            bg = '#DC2626'; // Pure solid red
            title = 'Deleted — ' + escapeHtml(masterName);
            msgHtml = '<strong>' + subjectHtml + '</strong> has been <strong>deleted</strong> from <strong>' + escapeHtml(masterName) + '</strong>.';
            bigIcon = '<svg viewBox="0 0 24 24" width="18" height="18" fill="none" stroke="#ffffff" stroke-width="2.2" stroke-linecap="round" stroke-linejoin="round"><polyline points="3 6 5 6 21 6"/><path d="M19 6v14a2 2 0 0 1-2 2H7a2 2 0 0 1-2-2V6m3 0V4a2 2 0 0 1 2-2h4a2 2 0 0 1 2 2v2"/><line x1="10" y1="11" x2="10" y2="17"/><line x1="14" y1="11" x2="14" y2="17"/></svg>';
            smallIcon = '<svg viewBox="0 0 24 24" width="14" height="14" fill="none" stroke="#ffffff" stroke-width="2.2" stroke-linecap="round" stroke-linejoin="round"><path d="M3 6h18m-2 0v14a2 2 0 0 1-2 2H7a2 2 0 0 1-2-2V6m3 0V4a2 2 0 0 1 2-2h4a2 2 0 0 1 2 2v2"/></svg>';
        } else { // error or notice
            bg = (action === 'error' ? '#DC2626' : '#2563EB');
            title = opts.title || (action === 'error' ? 'Error — ' + escapeHtml(masterName) : 'Notice — ' + escapeHtml(masterName));
            msgHtml = escapeHtml(opts.message || '');
            bigIcon = '<svg viewBox="0 0 24 24" width="18" height="18" fill="none" stroke="#ffffff" stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round"><circle cx="12" cy="12" r="10"/><line x1="12" y1="8" x2="12" y2="12"/><line x1="12" y1="16" x2="12.01" y2="16"/></svg>';
            smallIcon = bigIcon;
        }

        var container = document.getElementById('smard-toast-container');
        if (!container) {
            container = document.createElement('div');
            container.id = 'smard-toast-container';
            document.body.appendChild(container);
        }

        var toast = document.createElement('div');
        toast.className = 'smard-toast-card';
        toast.style.background = bg;

        toast.innerHTML =
            '<div class="smard-toast-icon-circle">' + bigIcon + '</div>' +
            '<div class="smard-toast-content">' +
                '<div class="smard-toast-title-row">' +
                    '<span class="smard-toast-title-icon">' + smallIcon + '</span>' +
                    '<span class="smard-toast-title-text">' + title + '</span>' +
                '</div>' +
                '<div class="smard-toast-msg">' + msgHtml + '</div>' +
            '</div>' +
            '<button type="button" class="smard-toast-close" title="Dismiss" aria-label="Close">&times;</button>';

        function dismissToast() {
            if (!toast || toast.classList.contains('hide')) return;
            toast.classList.remove('show');
            toast.classList.add('hide');
            setTimeout(function () {
                if (toast && toast.parentNode) toast.parentNode.removeChild(toast);
            }, 350);
        }

        var closeBtn = toast.querySelector('.smard-toast-close');
        if (closeBtn) {
            closeBtn.onclick = function (e) {
                e.stopPropagation();
                dismissToast();
            };
        }

        container.appendChild(toast);
        requestAnimationFrame(function () {
            toast.classList.add('show');
        });

        var timer = setTimeout(dismissToast, 4500);
        toast.onmouseenter = function () { clearTimeout(timer); };
        toast.onmouseleave = function () { timer = setTimeout(dismissToast, 2500); };
    }
    window.showActionToast = showActionToast;

    // Toast notifications wrapper
    function showToast(title, msg, type) {
        if (type === 'create' || type === 'created') {
            showActionToast('created', { title: title, message: msg });
        } else if (type === 'update' || type === 'updated') {
            showActionToast('updated', { title: title, message: msg });
        } else if (type === 'delete' || type === 'deleted') {
            showActionToast('deleted', { title: title, message: msg });
        } else if (type === 'error') {
            showActionToast('error', { title: title, message: msg });
        } else {
            showActionToast('info', { title: title, message: msg });
        }
    }

    function escapeHtml(str) {
        if (!str) return '';
        return String(str)
            .replace(/&/g, '&amp;')
            .replace(/</g, '&lt;')
            .replace(/>/g, '&gt;')
            .replace(/"/g, '&quot;')
            .replace(/'/g, '&#39;');
    }

    function formatAuditDate(val) {
        if (!val) return '-';
        if (typeof val === 'string' && /^\d{2}-[A-Za-z]{3}-\d{4}/.test(val.trim())) {
            return val.trim();
        }
        try {
            var d = new Date(val);
            if (isNaN(d.getTime())) return String(val);
            var months = ['Jan','Feb','Mar','Apr','May','Jun','Jul','Aug','Sep','Oct','Nov','Dec'];
            var day = ('0' + d.getDate()).slice(-2);
            var month = months[d.getMonth()];
            var year = d.getFullYear();
            var hours = d.getHours();
            var mins = ('0' + d.getMinutes()).slice(-2);
            var ampm = hours >= 12 ? 'PM' : 'AM';
            var h12 = hours % 12;
            h12 = h12 ? ('0' + h12).slice(-2) : '12';
            return day + '-' + month + '-' + year + ' ' + h12 + ':' + mins + ' ' + ampm;
        } catch (e) {
            return String(val);
        }
    }

    // Render or update Created / Updated Audit Bar below tables
    function updateAuditBar(t, info) {
        var block = document.getElementById('wps-type-block-' + t);
        if (!block) return;

        var createdBy = info.createdBy || 'Admin';
        var createdOn = info.createdOn ? formatAuditDate(info.createdOn) : '';

        // Determine whether this record has actually been updated (not newly created)
        var hasBeenUpdated = false;
        var updatedBy = '';
        var updatedOn = '';

        if (info.isUpdate === true || info.isUpdated === true) {
            hasBeenUpdated = true;
            updatedBy = info.updatedBy || '';
            updatedOn = info.updatedOn ? formatAuditDate(info.updatedOn) : '';
        } else if (info.updatedOn && info.updatedBy) {
            var cDate = createdOn;
            var uDate = formatAuditDate(info.updatedOn);
            // If creation date and update date differ, it was updated later
            if (cDate && uDate && cDate !== uDate) {
                hasBeenUpdated = true;
                updatedBy = info.updatedBy;
                updatedOn = uDate;
            }
        }

        var bar = block.querySelector('.type-audit-bar');
        if (!bar) {
            bar = document.createElement('div');
            bar.className = 'type-audit-bar';
            bar.id = 'type-audit-bar-' + t;

            // Place right before the type-action-bar
            var actionBar = block.querySelector('.type-action-bar');
            if (actionBar) {
                block.insertBefore(bar, actionBar);
            } else {
                block.appendChild(bar);
            }
        } else {
            bar.id = 'type-audit-bar-' + t;
        }

        var itemsHtml = '';
        if (createdBy) {
            itemsHtml += '<span class="audit-item"><strong class="audit-lbl">CREATED BY:</strong> <span class="audit-val">' + escapeHtml(createdBy) + '</span></span>';
        }
        if (createdOn) {
            itemsHtml += '<span class="audit-item"><strong class="audit-lbl">CREATED ON:</strong> <span class="audit-val">' + escapeHtml(createdOn) + '</span></span>';
        }

        // Only show UPDATED BY and UPDATED ON if the record has actually been updated
        if (hasBeenUpdated && updatedBy && updatedOn) {
            itemsHtml += '<span class="audit-item"><strong class="audit-lbl">UPDATED BY:</strong> <span class="audit-val">' + escapeHtml(updatedBy) + '</span></span>' +
                         '<span class="audit-item"><strong class="audit-lbl">UPDATED ON:</strong> <span class="audit-val">' + escapeHtml(updatedOn) + '</span></span>';
        }

        bar.innerHTML =
            '<div class="audit-icon-wrap">' +
                '<svg viewBox="0 0 24 24" width="20" height="20" fill="none" stroke="#16A34A" stroke-width="2.2" stroke-linecap="round" stroke-linejoin="round">' +
                    '<circle cx="12" cy="12" r="10"></circle>' +
                    '<polyline points="12 6 12 12 16 14"></polyline>' +
                '</svg>' +
            '</div>' +
            '<div class="audit-items-wrap">' +
                itemsHtml +
            '</div>';

        bar.style.display = 'flex';
    }
    window.updateAuditBar = updateAuditBar;

    // Lock or unlock inputs in TYPE block `t`
    function setBlockEditable(t, editable) {
        var block = document.getElementById('wps-type-block-' + t);
        if (!block) return;

        if (editable) {
            block.classList.remove('type-locked');
            block.classList.add('type-editing');
        } else {
            block.classList.add('type-locked');
            block.classList.remove('type-editing');
        }

        // Handle text/number inputs, selects, and textareas
        var inputs = block.querySelectorAll('input:not([type="button"]):not([type="submit"]), textarea, select');
        inputs.forEach(function (inp) {
            if (!editable) {
                inp.setAttribute('readonly', 'readonly');
                inp.style.pointerEvents = 'none';
                inp.setAttribute('tabindex', '-1');
                // Store placeholder in data-placeholder and clear so empty inputs display completely blank
                if (inp.placeholder) {
                    if (!inp.getAttribute('data-placeholder')) {
                        inp.setAttribute('data-placeholder', inp.placeholder);
                    }
                    inp.placeholder = '';
                }
            } else {
                // Restore placeholder for editing
                if (inp.getAttribute('data-placeholder')) {
                    inp.placeholder = inp.getAttribute('data-placeholder');
                }
                if (!inp.classList.contains('ct-input-ro') && !inp.classList.contains('ct-amount')) {
                    inp.removeAttribute('readonly');
                    inp.style.pointerEvents = '';
                    inp.removeAttribute('tabindex');
                } else {
                    inp.setAttribute('readonly', 'readonly');
                    inp.style.pointerEvents = 'none';
                    inp.setAttribute('tabindex', '-1');
                }
            }
        });
    }
    window.setBlockEditable = setBlockEditable;

    // Switch action button between Save (green) and Edit (blue)
    function renderTypeButton(t, isSaved) {
        var block = document.getElementById('wps-type-block-' + t);
        if (!block) return;

        var btn = block.querySelector('#type-save-btn-' + t) ||
                  block.querySelector('.type-action-btn-main') ||
                  block.querySelector('.type-save-btn, .type-edit-btn');
        if (!btn) return;

        btn.classList.add('type-action-btn-main');

        if (isSaved) {
            btn.className = 'type-edit-btn type-action-btn-main';
            btn.id = 'type-save-btn-' + t;
            btn.innerHTML =
                '<svg viewBox="0 0 24 24" width="14" height="14" fill="none" stroke="currentColor" stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round">' +
                    '<path d="M11 4H4a2 2 0 0 0-2 2v14a2 2 0 0 0 2 2h14a2 2 0 0 0 2-2v-7"/>' +
                    '<path d="M18.5 2.5a2.121 2.121 0 0 1 3 3L12 15l-4 1 1-4 9.5-9.5z"/>' +
                '</svg> Edit';
            btn.setAttribute('onclick', 'editType(' + t + ')');
            btn.onclick = function (e) {
                if (e) e.preventDefault();
                window.editType(t);
            };
        } else {
            btn.className = 'type-save-btn type-action-btn-main';
            btn.id = 'type-save-btn-' + t;
            btn.innerHTML =
                '<svg viewBox="0 0 24 24" width="14" height="14" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">' +
                    '<path d="M19 21H5a2 2 0 01-2-2V5a2 2 0 012-2h11l5 5v11a2 2 0 01-2 2z"/>' +
                    '<polyline points="17 21 17 13 7 13 7 21"/>' +
                    '<polyline points="7 3 7 8 15 8"/>' +
                '</svg> Save';
            btn.setAttribute('onclick', 'saveType(' + t + ')');
            btn.onclick = function (e) {
                if (e) e.preventDefault();
                window.saveType(t);
            };
        }
    }
    window.renderTypeButton = renderTypeButton;

    // Enter Edit Mode for TYPE block `t`
    window.editType = function (t) {
        var block = document.getElementById('wps-type-block-' + t);
        if (!block) return;

        // Snapshot current data state when entering edit mode to detect if any changes were made
        block._originalSnapshot = JSON.stringify(extractComparablePayload(t));

        currentActiveDockType = t;
        setBlockEditable(t, true);
        renderTypeButton(t, false);
        if (typeof updateFloatingDock === 'function') updateFloatingDock();
        showToast('Edit Mode', 'You can now edit TYPE ' + t + '. Click Save when done.', 'success');

        var firstInp = block.querySelector('input:not([readonly]):not([type="button"]), textarea');
        if (firstInp) {
            firstInp.focus();
        }
    };

    // Helper to reliably extract inputs from a cost table row (Material or Labour)
    // Works whether the row is currently editable or locked (readonly)
    function getCostRowInputs(row) {
        if (!row) return null;
        var cells = row.cells;
        if (cells && cells.length >= 8) {
            return {
                descInput: cells[1] ? cells[1].querySelector('input, textarea') : null,
                specInput: cells[2] ? cells[2].querySelector('input, textarea') : null,
                unitInput: cells[3] ? cells[3].querySelector('input, textarea') : null,
                qtyInput: cells[4] ? cells[4].querySelector('input') : null,
                rateInput: cells[5] ? cells[5].querySelector('input') : null,
                amtInput: cells[6] ? cells[6].querySelector('input') : null,
                remarksInput: cells[7] ? cells[7].querySelector('input, textarea') : null
            };
        }
        var allInputs = Array.prototype.slice.call(row.querySelectorAll('input:not([type="button"]):not([type="submit"]), textarea'));
        var amt = row.querySelector('.ct-amount, .ct-input-ro') || null;
        var numInputs = row.querySelectorAll('input[type="number"]');
        var textInputs = allInputs.filter(function (inp) {
            return inp.type !== 'number' && inp !== amt;
        });
        return {
            descInput: textInputs[0] || null,
            specInput: textInputs[1] || null,
            unitInput: textInputs[2] || null,
            qtyInput: numInputs[0] || null,
            rateInput: numInputs[1] || null,
            amtInput: amt || (allInputs.length >= 7 ? allInputs[5] : null),
            remarksInput: textInputs[3] || null
        };
    }
    window.getCostRowInputs = getCostRowInputs;

    // Extract table rows (Material or Labour)
    function extractCostRows(tbody) {
        var items = [];
        if (!tbody) return items;
        for (var i = 0; i < tbody.rows.length; i++) {
            var tr = tbody.rows[i];
            var inputs = getCostRowInputs(tr);
            if (!inputs) continue;

            var desc = inputs.descInput ? inputs.descInput.value.trim() : '';
            var spec = inputs.specInput ? inputs.specInput.value.trim() : '';
            var unit = inputs.unitInput ? inputs.unitInput.value.trim() : '';
            var rem = inputs.remarksInput ? inputs.remarksInput.value.trim() : '';
            var qty = inputs.qtyInput ? parseFloat(inputs.qtyInput.value) || 0 : 0;
            var rate = inputs.rateInput ? parseFloat(inputs.rateInput.value) || 0 : 0;
            var amt = inputs.amtInput ? parseFloat(inputs.amtInput.value) || 0 : (qty * rate);

            if (desc || spec || unit || qty > 0 || rate > 0 || amt > 0 || rem) {
                items.push({
                    slNo: i + 1,
                    itemDescription: desc,
                    specification: spec,
                    unit: unit,
                    qty: qty,
                    unitRate: rate,
                    amount: amt,
                    remarks: rem
                });
            }
        }
        return items;
    }

    // Populate rows from DB
    function populateCostRows(tbody, items, t, type) {
        if (!tbody) return;
        tbody.innerHTML = '';
        var addFn = type === 'Material'
            ? (window.buildMatRow || (typeof buildMatRow === 'function' ? buildMatRow : null))
            : (window.buildLabRow || (typeof buildLabRow === 'function' ? buildLabRow : null));
        var rowCounter = type === 'Material' ? (window.matRowCounts || matRowCounts) : (window.labRowCounts || labRowCounts);
        if (rowCounter) rowCounter[t] = 0;

        if (!items || items.length === 0) {
            if (rowCounter) rowCounter[t] = 1;
            if (typeof addFn === 'function') {
                tbody.appendChild(addFn(t, 1));
            }
            return;
        }

        items.forEach(function (item, idx) {
            var rowNum = idx + 1;
            if (rowCounter) rowCounter[t] = rowNum;
            if (typeof addFn === 'function') {
                var tr = addFn(t, rowNum);
                tbody.appendChild(tr);

                var inputs = getCostRowInputs(tr);
                if (inputs) {
                    if (inputs.descInput) inputs.descInput.value = item.itemDescription || '';
                    if (inputs.specInput) inputs.specInput.value = item.specification || '';
                    if (inputs.unitInput) inputs.unitInput.value = item.unit || '';
                    if (inputs.remarksInput) inputs.remarksInput.value = item.remarks || '';
                    if (inputs.qtyInput) inputs.qtyInput.value = (item.qty !== null && item.qty !== undefined && parseFloat(item.qty) > 0) ? item.qty : '';
                    if (inputs.rateInput) inputs.rateInput.value = (item.unitRate !== null && item.unitRate !== undefined && parseFloat(item.unitRate) > 0) ? item.unitRate : '';
                    var amtVal = parseFloat(item.amount);
                    if (isNaN(amtVal) || amtVal === 0) {
                        amtVal = (parseFloat(item.qty) || 0) * (parseFloat(item.unitRate) || 0);
                    }
                    if (inputs.amtInput) inputs.amtInput.value = (amtVal || 0).toFixed(2);
                }
            }
        });
    }

    // Populate TYPE block `t` with data from DB record `rec`
    function populateBlockWithData(t, rec) {
        if (!rec) return;
        var block = document.getElementById('wps-type-block-' + t);
        if (!block) return;

        if (rec.id) {
            block.setAttribute('data-id', rec.id);
        }

        // 1. Type Name
        var typeInp = document.getElementById('wps-type-' + t);
        if (typeInp) {
            typeInp.value = rec.typeName || '';
        }

        // 2. System Description / Specification
        var specInp = document.getElementById('wps-spec-' + t);
        if (specInp) {
            specInp.value = rec.systemDescription || '';
        }

        // 3. Dimensions Length, Height & Area
        var lenInp = document.getElementById('wps-len-' + t);
        if (lenInp) {
            lenInp.value = (rec.dimensionLength !== null && rec.dimensionLength !== undefined && parseFloat(rec.dimensionLength) > 0) ? rec.dimensionLength : '';
        }
        var htInp = document.getElementById('wps-ht-' + t);
        if (htInp) {
            htInp.value = (rec.dimensionHeight !== null && rec.dimensionHeight !== undefined && parseFloat(rec.dimensionHeight) > 0) ? rec.dimensionHeight : '';
        }
        var areaEl = document.getElementById('wps-area-' + t);
        if (areaEl) {
            var areaVal = (rec.dimensionArea !== null && rec.dimensionArea !== undefined && parseFloat(rec.dimensionArea) > 0) ? parseFloat(rec.dimensionArea) : 0;
            if ((!areaVal || isNaN(areaVal)) && lenInp && htInp) {
                var lVal = parseFloat(lenInp.value) || 0;
                var hVal = parseFloat(htInp.value) || 0;
                areaVal = lVal * hVal;
            }
            areaEl.textContent = (areaVal || 0).toFixed(2);
        }

        // 4. Material Costs
        var matTbody = document.getElementById('mat-tbody-' + t);
        if (matTbody) {
            populateCostRows(matTbody, rec.materialCosts, t, 'Material');
        }

        // 5. Labour Costs
        var labTbody = document.getElementById('lab-tbody-' + t);
        if (labTbody) {
            populateCostRows(labTbody, rec.labourCosts, t, 'Labour');
        }

        // 6. With Profit Percentages & Remarks
        var pctTrans = document.getElementById('pct-transport-' + t);
        if (pctTrans && rec.transportationPercent !== undefined && rec.transportationPercent !== null) {
            pctTrans.value = rec.transportationPercent;
        }
        var pTransRow = pctTrans ? pctTrans.closest('tr') : null;
        if (pTransRow && pTransRow.querySelector('.profit-remark')) {
            pTransRow.querySelector('.profit-remark').value = rec.transportationRemarks || '';
        }

        var pctWater = document.getElementById('pct-water-' + t);
        if (pctWater && rec.waterElectricityPercent !== undefined && rec.waterElectricityPercent !== null) {
            pctWater.value = rec.waterElectricityPercent;
        }
        var pWaterRow = pctWater ? pctWater.closest('tr') : null;
        if (pWaterRow && pWaterRow.querySelector('.profit-remark')) {
            pWaterRow.querySelector('.profit-remark').value = rec.waterElectricityRemarks || '';
        }

        var pctTools = document.getElementById('pct-tools-' + t);
        if (pctTools && rec.toolsEquipmentPercent !== undefined && rec.toolsEquipmentPercent !== null) {
            pctTools.value = rec.toolsEquipmentPercent;
        }
        var pToolsRow = pctTools ? pctTools.closest('tr') : null;
        if (pToolsRow && pToolsRow.querySelector('.profit-remark')) {
            pToolsRow.querySelector('.profit-remark').value = rec.toolsEquipmentRemarks || '';
        }

        var pctOH = document.getElementById('pct-oh-' + t);
        if (pctOH && rec.profitPercent !== undefined && rec.profitPercent !== null) {
            pctOH.value = rec.profitPercent;
        }

        // 7. Recalculate Totals
        var recalcMat = window.recalcMatTotalsFor || (typeof recalcMatTotalsFor === 'function' ? recalcMatTotalsFor : null);
        if (recalcMat) recalcMat(t);

        var recalcLab = window.recalcLabTotalsFor || (typeof recalcLabTotalsFor === 'function' ? recalcLabTotalsFor : null);
        if (recalcLab) recalcLab(t);

        var recalcTotal = window.recalcTotalMatLabFor || (typeof recalcTotalMatLabFor === 'function' ? recalcTotalMatLabFor : null);
        if (recalcTotal) recalcTotal(t);

        var recalcProf = window.recalcProfit || (typeof recalcProfit === 'function' ? recalcProfit : null);
        if (recalcProf) recalcProf(t);

        // Ensure database totals match if provided
        if (rec.materialTotalAmount !== undefined && rec.materialTotalAmount !== null && parseFloat(rec.materialTotalAmount) > 0) {
            var mTot = document.getElementById('mat-total-' + t);
            if (mTot) mTot.textContent = parseFloat(rec.materialTotalAmount).toFixed(2);
        }
        if (rec.materialTotalPerSquareMeter !== undefined && rec.materialTotalPerSquareMeter !== null && parseFloat(rec.materialTotalPerSquareMeter) > 0) {
            var mM2 = document.getElementById('mat-per-m2-' + t);
            if (mM2) mM2.textContent = parseFloat(rec.materialTotalPerSquareMeter).toFixed(2);
        }
        if (rec.labourTotalAmount !== undefined && rec.labourTotalAmount !== null && parseFloat(rec.labourTotalAmount) > 0) {
            var lTot = document.getElementById('lab-total-' + t);
            if (lTot) lTot.textContent = parseFloat(rec.labourTotalAmount).toFixed(2);
        }
        if (rec.labourTotalPerSquareMeter !== undefined && rec.labourTotalPerSquareMeter !== null && parseFloat(rec.labourTotalPerSquareMeter) > 0) {
            var lM2 = document.getElementById('lab-per-m2-' + t);
            if (lM2) lM2.textContent = parseFloat(rec.labourTotalPerSquareMeter).toFixed(2);
        }
        if (rec.totalRateAmount !== undefined && rec.totalRateAmount !== null && parseFloat(rec.totalRateAmount) > 0) {
            var totRate = document.getElementById('amt-total-rate-' + t);
            if (totRate) totRate.textContent = parseFloat(rec.totalRateAmount).toFixed(2);
            var warn = document.getElementById('total-rate-warn-' + t);
            if (warn) warn.style.display = 'none';
        }

        // 8. Update Audit Info Bar
        updateAuditBar(t, rec);

        // 9. Put block into locked/saved state with Edit button
        setBlockEditable(t, false);
        renderTypeButton(t, true);

        // 10. Store snapshot for change tracking
        block._originalSnapshot = JSON.stringify(extractComparablePayload(t));
    }
    window.populateBlockWithData = populateBlockWithData;

    // Copy Material or Labour Cost details from previous type (TYPE t-1) into current type (TYPE t)
    function copyCostDetails(costType, t) {
        var curBlock = document.getElementById('wps-type-block-' + t);
        var prevBlock = curBlock ? curBlock.previousElementSibling : null;
        var srcT = t - 1;
        if (prevBlock && prevBlock.id && prevBlock.id.indexOf('wps-type-block-') === 0) {
            var pNum = parseInt(prevBlock.id.replace('wps-type-block-', ''), 10);
            if (!isNaN(pNum)) srcT = pNum;
        }

        var srcTbody = document.getElementById(costType + '-tbody-' + srcT);
        var destTbody = document.getElementById(costType + '-tbody-' + t);
        if (!srcTbody || !destTbody) return;

        var srcRows = srcTbody.querySelectorAll('tr');
        if (srcRows.length === 0) return;

        var rowDataList = [];
        srcRows.forEach(function (row) {
            var inputs = getCostRowInputs(row);
            if (!inputs) return;
            var qVal = inputs.qtyInput ? inputs.qtyInput.value : '';
            var rVal = inputs.rateInput ? inputs.rateInput.value : '';
            var aVal = inputs.amtInput ? inputs.amtInput.value : '';
            rowDataList.push({
                desc: inputs.descInput ? inputs.descInput.value : '',
                spec: inputs.specInput ? inputs.specInput.value : '',
                unit: inputs.unitInput ? inputs.unitInput.value : '',
                qnty: qVal,
                rate: rVal,
                amt: aVal,
                remarks: inputs.remarksInput ? inputs.remarksInput.value : ''
            });
        });

        destTbody.innerHTML = '';
        var rowCounter = (costType === 'mat' ? (window.matRowCounts || matRowCounts) : (window.labRowCounts || labRowCounts)) || {};
        rowCounter[t] = 0;

        var addFn = (costType === 'mat') ? (window.buildMatRow || buildMatRow) : (window.buildLabRow || buildLabRow);

        rowDataList.forEach(function (data, idx) {
            var rIdx = idx + 1;
            var newRow = addFn(t, rIdx);
            var inputs = getCostRowInputs(newRow);

            if (inputs) {
                if (inputs.descInput) inputs.descInput.value = data.desc || '';
                if (inputs.specInput) inputs.specInput.value = data.spec || '';
                if (inputs.unitInput) inputs.unitInput.value = data.unit || '';
                if (inputs.remarksInput) inputs.remarksInput.value = data.remarks || '';

                if (inputs.qtyInput) inputs.qtyInput.value = (data.qnty !== '' && data.qnty !== null && data.qnty !== undefined) ? data.qnty : '';
                if (inputs.rateInput) inputs.rateInput.value = (data.rate !== '' && data.rate !== null && data.rate !== undefined) ? data.rate : '';

                var qVal = parseFloat(data.qnty);
                var rVal = parseFloat(data.rate);
                var amt = (!isNaN(qVal) && !isNaN(rVal)) ? (qVal * rVal) : (parseFloat(data.amt) || 0);
                if (inputs.amtInput) inputs.amtInput.value = amt.toFixed(2);
            }

            destTbody.appendChild(newRow);
        });

        rowCounter[t] = rowDataList.length;

        // If current block is in locked state, ensure copied rows match locked state
        if (curBlock && curBlock.classList.contains('type-locked')) {
            if (typeof setBlockEditable === 'function') setBlockEditable(t, false);
        }

        if (costType === 'mat') {
            var recalcMat = window.recalcMatTotalsFor || recalcMatTotalsFor;
            if (typeof recalcMat === 'function') recalcMat(t);
        } else {
            var recalcLab = window.recalcLabTotalsFor || recalcLabTotalsFor;
            if (typeof recalcLab === 'function') recalcLab(t);
        }

        var recalcTotal = window.recalcTotalMatLabFor || recalcTotalMatLabFor;
        if (typeof recalcTotal === 'function') recalcTotal(t);

        var recalcProf = window.recalcProfit || recalcProfit;
        if (typeof recalcProf === 'function') recalcProf(t);

        var btn = document.getElementById(costType + '-copy-btn-' + t);
        if (btn) {
            btn.innerHTML = '<svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2.5" style="width:14px;height:14px;"><polyline points="20 6 9 17 4 12" stroke-linecap="round" stroke-linejoin="round"/></svg> Copied!';
            btn.style.background = '#046A38';
            setTimeout(function () {
                setCopyButtonState(costType, t, true);
            }, 600);
        } else {
            setCopyButtonState(costType, t, true);
        }
    }
    window.copyCostDetails = copyCostDetails;

    // Helper to get previous/source type block number
    function getSourceTypeNumber(t) {
        var curBlock = document.getElementById('wps-type-block-' + t);
        var prevBlock = curBlock ? curBlock.previousElementSibling : null;
        var srcT = t - 1;
        if (prevBlock && prevBlock.id && prevBlock.id.indexOf('wps-type-block-') === 0) {
            var pNum = parseInt(prevBlock.id.replace('wps-type-block-', ''), 10);
            if (!isNaN(pNum)) srcT = pNum;
        }
        return srcT > 0 ? srcT : 1;
    }
    window.getSourceTypeNumber = getSourceTypeNumber;

    // Toggle button state between "Copy from TYPE X" and "Clear table"
    function setCopyButtonState(costType, t, isClearMode) {
        var btn = document.getElementById(costType + '-copy-btn-' + t);
        if (!btn) return;

        var copySvg = '<svg viewBox="0 0 16 16" fill="currentColor" style="width:14px;height:14px;"><path d="M4 2a2 2 0 0 1 2-2h8a2 2 0 0 1 2 2v8a2 2 0 0 1-2 2H6a2 2 0 0 1-2-2zm2-1a1 1 0 0 0-1 1v8a1 1 0 0 0 1 1h8a1 1 0 0 0 1-1V2a1 1 0 0 0-1-1zM2 5a1 1 0 0 0-1 1v8a1 1 0 0 0 1 1h8a1 1 0 0 0 1-1v-1h1v1a2 2 0 0 1-2 2H2a2 2 0 0 1-2-2V6a2 2 0 0 1 2-2h1v1z"/></svg>';
        var clearSvg = '<svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" style="width:14px;height:14px;"><polyline points="3 6 5 6 21 6"/><path d="M19 6v14a2 2 0 0 1-2 2H7a2 2 0 0 1-2-2V6m3 0V4a2 2 0 0 1 2-2h4a2 2 0 0 1 2 2v2"/><line x1="10" y1="11" x2="10" y2="17"/><line x1="14" y1="11" x2="14" y2="17"/></svg>';

        if (isClearMode) {
            btn.className = 'cost-copy-btn cost-clear-btn';
            btn.style.background = '';
            btn.innerHTML = clearSvg + ' Clear table';
            btn.title = 'Clear table';
            btn.setAttribute('onclick', "clearCostDetails('" + costType + "'," + t + ")");
            btn.onclick = function (e) {
                if (e) e.preventDefault();
                window.clearCostDetails(costType, t);
            };
        } else {
            var srcT = getSourceTypeNumber(t);
            btn.className = 'cost-copy-btn';
            btn.style.background = '';
            btn.innerHTML = copySvg + ' Copy from TYPE ' + srcT;
            btn.title = 'Copy from TYPE ' + srcT;
            btn.setAttribute('onclick', "copyCostDetails('" + costType + "'," + t + ")");
            btn.onclick = function (e) {
                if (e) e.preventDefault();
                window.copyCostDetails(costType, t);
            };
        }
    }
    window.setCopyButtonState = setCopyButtonState;

    // Clear Material or Labour Cost table and revert button to "Copy from TYPE X"
    function clearCostDetails(costType, t) {
        var destTbody = document.getElementById(costType + '-tbody-' + t);
        if (!destTbody) return;

        destTbody.innerHTML = '';
        var rowCounter = (costType === 'mat' ? (window.matRowCounts || matRowCounts) : (window.labRowCounts || labRowCounts)) || {};
        rowCounter[t] = 1;

        var addFn = (costType === 'mat') ? (window.buildMatRow || buildMatRow) : (window.buildLabRow || buildLabRow);
        if (typeof addFn === 'function') {
            destTbody.appendChild(addFn(t, 1));
        }

        if (costType === 'mat') {
            var recalcMat = window.recalcMatTotalsFor || recalcMatTotalsFor;
            if (typeof recalcMat === 'function') recalcMat(t);
        } else {
            var recalcLab = window.recalcLabTotalsFor || recalcLabTotalsFor;
            if (typeof recalcLab === 'function') recalcLab(t);
        }

        var recalcTotal = window.recalcTotalMatLabFor || recalcTotalMatLabFor;
        if (typeof recalcTotal === 'function') recalcTotal(t);

        var recalcProf = window.recalcProfit || recalcProfit;
        if (typeof recalcProf === 'function') recalcProf(t);

        setCopyButtonState(costType, t, false);

        if (typeof showToast === 'function') {
            showToast('Table Cleared', (costType === 'mat' ? 'Material' : 'Labour') + ' Cost table has been cleared.', 'info');
        }
    }
    window.clearCostDetails = clearCostDetails;

    // Global Limit Check: only keep numbers under maxVal (10000 / 100), never force maxVal into input
    function checkLimit(input, maxVal, unit) {
        if (!input || input.value === '') {
            if (input) input.setAttribute('data-last-valid', '');
            return true;
        }
        var val = parseFloat(input.value);
        if (isNaN(val)) return true;
        if (val < 0) {
            input.value = '0';
            input.setAttribute('data-last-valid', '0');
            return true;
        }
        if (val > maxVal) {
            var lastVal = input.getAttribute('data-last-valid');
            input.value = (lastVal !== null && lastVal !== undefined && lastVal !== '') ? lastVal : '';
            var msg = (unit === '%' || maxVal === 100)
                ? "can't exceed more than 100"
                : "can't exceed more than 10000";
            var warnFn = window.showLimitWarning || (typeof showLimitWarning === 'function' ? showLimitWarning : null);
            if (warnFn) warnFn(input, msg);
            return false;
        }
        input.setAttribute('data-last-valid', input.value);
        return true;
    }
    window.checkLimit = checkLimit;

    // Extract complete data payload from TYPE block `t`
    function extractMasterData(t) {
        var block = document.getElementById('wps-type-block-' + t);
        if (!block) return null;

        var idVal = parseInt(block.getAttribute('data-id') || '0', 10);
        var typeName = (document.getElementById('wps-type-' + t) || {}).value || '';
        var systemDescription = (document.getElementById('wps-spec-' + t) || {}).value || '';
        var len = parseFloat((document.getElementById('wps-len-' + t) || {}).value) || 0;
        var ht = parseFloat((document.getElementById('wps-ht-' + t) || {}).value) || 0;
        var area = parseFloat((document.getElementById('wps-area-' + t) || {}).textContent) || 0;

        var matTotal = parseFloat((document.getElementById('mat-total-' + t) || {}).textContent) || 0;
        var matPerM2 = parseFloat((document.getElementById('mat-per-m2-' + t) || {}).textContent) || 0;
        var labTotal = parseFloat((document.getElementById('lab-total-' + t) || {}).textContent) || 0;
        var labPerM2 = parseFloat((document.getElementById('lab-per-m2-' + t) || {}).textContent) || 0;
        var grandTotal = parseFloat((document.getElementById('total-mat-lab-val-' + t) || {}).textContent) || 0;

        var pTrans = parseFloat((document.getElementById('pct-transport-' + t) || {}).value) || 0;
        var aTrans = parseFloat((document.getElementById('amt-transport-' + t) || {}).textContent) || 0;
        var pTransRow = document.getElementById('pct-transport-' + t)?.closest('tr');
        var rTrans = pTransRow ? (pTransRow.querySelector('.profit-remark')?.value || '') : '';

        var pWater = parseFloat((document.getElementById('pct-water-' + t) || {}).value) || 0;
        var aWater = parseFloat((document.getElementById('amt-water-' + t) || {}).textContent) || 0;
        var pWaterRow = document.getElementById('pct-water-' + t)?.closest('tr');
        var rWater = pWaterRow ? (pWaterRow.querySelector('.profit-remark')?.value || '') : '';

        var pTools = parseFloat((document.getElementById('pct-tools-' + t) || {}).value) || 0;
        var aTools = parseFloat((document.getElementById('amt-tools-' + t) || {}).textContent) || 0;
        var pToolsRow = document.getElementById('pct-tools-' + t)?.closest('tr');
        var rTools = pToolsRow ? (pToolsRow.querySelector('.profit-remark')?.value || '') : '';

        var pOH = parseFloat((document.getElementById('pct-oh-' + t) || {}).value) || 0;
        var aOH = parseFloat((document.getElementById('amt-oh-' + t) || {}).textContent) || 0;
        var totRate = parseFloat((document.getElementById('amt-total-rate-' + t) || {}).textContent) || 0;

        var matRows = extractCostRows(document.getElementById('mat-tbody-' + t));
        var labRows = extractCostRows(document.getElementById('lab-tbody-' + t));

        return {
            id: idVal > 0 ? idVal : null,
            typeName: typeName ? typeName.trim() : '',
            systemDescription: systemDescription,
            dimensionLength: len,
            dimensionHeight: ht,
            dimensionSize: (len > 0 || ht > 0) ? (len + ' x ' + ht) : '',
            dimensionArea: area,
            materialTotalAmount: matTotal,
            materialTotalPerSquareMeter: matPerM2,
            labourTotalAmount: labTotal,
            labourTotalPerSquareMeter: labPerM2,
            grandTotalPerSquareMeter: grandTotal,
            transportationPercent: pTrans,
            transportationAmount: aTrans,
            transportationRemarks: rTrans,
            waterElectricityPercent: pWater,
            waterElectricityAmount: aWater,
            waterElectricityRemarks: rWater,
            toolsEquipmentPercent: pTools,
            toolsEquipmentAmount: aTools,
            toolsEquipmentRemarks: rTools,
            profitPercent: pOH,
            profitAmount: aOH,
            totalRateAmount: totRate,
            isActive: true,
            materialCosts: matRows,
            labourCosts: labRows
        };
    }

    function normalizeCostsForComparison(costs) {
        if (!Array.isArray(costs)) return [];
        return costs
            .filter(function (c) {
                return (c.itemDescription && c.itemDescription.trim()) ||
                       (c.specification && c.specification.trim()) ||
                       (c.unit && c.unit.trim()) ||
                       (c.qty && Number(c.qty) > 0) ||
                       (c.unitRate && Number(c.unitRate) > 0) ||
                       (c.amount && Number(c.amount) > 0) ||
                       (c.remarks && c.remarks.trim());
            })
            .map(function (c) {
                return {
                    desc: (c.itemDescription || '').trim().toLowerCase(),
                    spec: (c.specification || '').trim().toLowerCase(),
                    unit: (c.unit || '').trim().toLowerCase(),
                    qty: Number(c.qty || 0),
                    rate: Number(c.unitRate || 0),
                    amt: Number(c.amount || 0),
                    rem: (c.remarks || '').trim().toLowerCase()
                };
            });
    }

    function extractComparablePayload(t) {
        var p = extractMasterData(t);
        if (!p) return {};
        return {
            typeName: (p.typeName || '').trim().toLowerCase(),
            desc: (p.systemDescription || '').trim().toLowerCase(),
            len: Number(p.dimensionLength || 0),
            ht: Number(p.dimensionHeight || 0),
            pTrans: Number(p.transportationPercent || 0),
            rTrans: (p.transportationRemarks || '').trim().toLowerCase(),
            pWater: Number(p.waterElectricityPercent || 0),
            rWater: (p.waterElectricityRemarks || '').trim().toLowerCase(),
            pTools: Number(p.toolsEquipmentPercent || 0),
            rTools: (p.toolsEquipmentRemarks || '').trim().toLowerCase(),
            pOH: Number(p.profitPercent || 0),
            mat: normalizeCostsForComparison(p.materialCosts),
            lab: normalizeCostsForComparison(p.labourCosts)
        };
    }

    // Save or Update handler
    window.saveType = async function (t) {
        var block = document.getElementById('wps-type-block-' + t);
        if (!block) return;

        // If block is currently in locked/saved state, clicking Edit action triggers edit mode
        if (block.classList.contains('type-locked')) {
            window.editType(t);
            return;
        }

        var btn = document.getElementById('type-save-btn-' + t) ||
                  block.querySelector('.type-action-btn-main') ||
                  block.querySelector('.type-save-btn, .type-edit-btn');

        var payload = extractMasterData(t);
        if (!payload) return;

        var controller = getControllerName();
        var masterName = getMasterDisplayName();

        var typeInp = document.getElementById('wps-type-' + t);
        var rawTypeName = typeInp ? typeInp.value.trim() : (payload.typeName || '');

        // 1. Mandatory validation: Type name cannot be empty
        if (!rawTypeName) {
            showActionToast('error', {
                masterName: masterName,
                message: 'Type Name is mandatory. Please enter a Type Name to save.'
            });
            if (typeInp) {
                typeInp.focus();
                typeInp.style.borderColor = '#DC2626';
                typeInp.style.boxShadow = '0 0 0 3px rgba(220, 38, 38, 0.2)';
                typeInp.scrollIntoView({ behavior: 'smooth', block: 'center' });
                setTimeout(function () {
                    typeInp.style.borderColor = '';
                    typeInp.style.boxShadow = '';
                }, 3500);
            }
            return;
        }

        // 2. Uniqueness validation: Check other blocks currently on page
        var allBlocks = document.querySelectorAll('.wps-type-block');
        var duplicateFound = false;
        allBlocks.forEach(function (b) {
            var otherT = parseInt(b.id.replace('wps-type-block-', ''), 10);
            if (otherT !== t) {
                var otherInp = document.getElementById('wps-type-' + otherT);
                if (otherInp && otherInp.value.trim().toLowerCase() === rawTypeName.toLowerCase()) {
                    duplicateFound = true;
                }
            }
        });

        if (duplicateFound) {
            showActionToast('error', {
                masterName: masterName,
                message: 'Type "' + rawTypeName + '" already exists in ' + masterName + '. Type name must be unique.'
            });
            if (typeInp) {
                typeInp.focus();
                typeInp.style.borderColor = '#DC2626';
                typeInp.style.boxShadow = '0 0 0 3px rgba(220, 38, 38, 0.2)';
                typeInp.scrollIntoView({ behavior: 'smooth', block: 'center' });
                setTimeout(function () {
                    typeInp.style.borderColor = '';
                    typeInp.style.boxShadow = '';
                }, 3500);
            }
            return;
        }

        // 3. Total Rate validation: Total Rate must be greater than 0
        if (!payload.totalRateAmount || payload.totalRateAmount <= 0) {
            showActionToast('error', {
                masterName: masterName,
                message: 'Total Rate cannot be 0.00. Please enter cost details before saving.'
            });
            var rateEl = document.getElementById('amt-total-rate-' + t) || document.getElementById('total-rate-warn-' + t);
            if (rateEl) {
                rateEl.scrollIntoView({ behavior: 'smooth', block: 'center' });
            }
            return;
        }

        payload.typeName = rawTypeName;

        var origBtnHtml = btn ? btn.innerHTML : '';
        if (btn) {
            btn.innerHTML = '<svg class="spin-icon" viewBox="0 0 24 24" width="14" height="14" fill="none" stroke="currentColor" stroke-width="2.5"><circle cx="12" cy="12" r="10" stroke-dasharray="32" stroke-linecap="round"/></svg> Saving...';
            btn.disabled = true;
        }

        var isUpdate = (payload.id && payload.id > 0);

        // If updating, check whether any fields were actually modified
        if (isUpdate && block._originalSnapshot) {
            var currentSnapshot = JSON.stringify(extractComparablePayload(t));
            if (block._originalSnapshot === currentSnapshot) {
                showActionToast('info', {
                    masterName: masterName,
                    title: 'No Changes — ' + masterName,
                    message: 'No changes done to update.'
                });
                setBlockEditable(t, false);
                renderTypeButton(t, true);
                if (typeof updateFloatingDock === 'function') updateFloatingDock();
                return;
            }
        }

        var url = '/' + controller + (isUpdate ? '/Update' : '/Save');

        try {
            var response = await fetch(url, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(payload)
            });

            var result = await response.json();

            if (response.ok && result.success) {
                if (result.noChanges) {
                    showActionToast('info', {
                        masterName: masterName,
                        title: 'No Changes — ' + masterName,
                        message: result.message || 'No changes done to update.'
                    });
                    setBlockEditable(t, false);
                    renderTypeButton(t, true);
                    if (typeof updateFloatingDock === 'function') updateFloatingDock();
                    return;
                }

                if (result.id) {
                    block.setAttribute('data-id', result.id);
                }

                // Update cached records
                if (!window.cachedMasterRecords) window.cachedMasterRecords = [];
                var savedRecord = Object.assign({}, payload, {
                    id: result.id || payload.id,
                    createdBy: result.createdBy,
                    createdOn: result.createdOn,
                    updatedBy: result.updatedBy,
                    updatedOn: result.updatedOn,
                    isUpdate: isUpdate
                });
                var cIdx = window.cachedMasterRecords.findIndex(function (r) { return r.id === savedRecord.id; });
                if (cIdx >= 0) {
                    window.cachedMasterRecords[cIdx] = savedRecord;
                } else {
                    window.cachedMasterRecords.push(savedRecord);
                }

                result.isUpdate = isUpdate;
                // Render or update audit info badge below tables
                updateAuditBar(t, result);

                if (btn) {
                    btn.innerHTML = '<svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2.5" width="14" height="14"><polyline points="20 6 9 17 4 12" stroke-linecap="round" stroke-linejoin="round"/></svg> Saved!';
                    btn.style.background = 'linear-gradient(135deg,#14532D,#15803D)';
                }

                // Display toast popup matching the design: Created or Updated
                showActionToast(isUpdate ? 'updated' : 'created', {
                    masterName: masterName,
                    typeName: rawTypeName,
                    typeIndex: t
                });

                // Immediately refresh notifications in navbar
                if (typeof loadNotifications === 'function') {
                    loadNotifications();
                }

                setTimeout(function () {
                    if (btn) {
                        btn.style.background = '';
                        btn.disabled = false;
                    }
                    // Transition to locked/saved state and switch to Edit button
                    setBlockEditable(t, false);
                    renderTypeButton(t, true);
                    block._originalSnapshot = JSON.stringify(extractComparablePayload(t));
                    if (typeof updateFloatingDock === 'function') updateFloatingDock();
                }, 1000);
            } else {
                showActionToast('error', {
                    masterName: masterName,
                    message: result.message || 'Failed to save record.'
                });
                if (btn) {
                    btn.innerHTML = origBtnHtml;
                    btn.disabled = false;
                }
            }
        } catch (err) {
            showActionToast('error', {
                masterName: masterName,
                message: err.message || 'Network error occurred.'
            });
            if (btn) {
                btn.innerHTML = origBtnHtml;
                btn.disabled = false;
            }
        }
    };

    // Hook deleteType to customize dialog if dbId > 0
    var origDeleteType = window.deleteType;
    window.deleteType = function(t) {
        if (typeof origDeleteType === 'function') {
            origDeleteType(t);
        }
        var block = document.getElementById('wps-type-block-' + t);
        if (!block) return;
        var dbId = parseInt(block.getAttribute('data-id') || '0', 10);
        var isPrivileged = window.currentUser && (window.currentUser.isAdmin || window.currentUser.isManager);
        var title = document.getElementById('del-modal-title');
        var desc = document.getElementById('del-modal-desc');
        var btns = document.getElementById('del-modal-btns');

        // Admin & Manager directly delete with NO approval required
        if (isPrivileged) {
            if (title) title.textContent = 'Delete TYPE ' + t + ' Section?';
            if (desc) desc.innerHTML = 'This will permanently remove the type section along with all its <strong>Material Cost</strong>, <strong>Labour Cost</strong> and <strong>Profit</strong> data. This action cannot be undone.';
            if (btns) {
                btns.innerHTML =
                    '<button type="button" class="del-btn-cancel" onclick="closeDelTypeModal()">' +
                      '<svg width="17" height="17" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2.2"><circle cx="12" cy="12" r="10"/><line x1="15" y1="9" x2="9" y2="15"/><line x1="9" y1="9" x2="15" y2="15"/></svg>' +
                      'Cancel' +
                    '</button>' +
                    '<button type="button" class="del-btn-confirm" id="del-confirm-btn" onclick="confirmDeleteType(false)">' +
                      '<svg width="17" height="17" viewBox="0 0 24 24" fill="currentColor"><path d="M6 19c0 1.1.9 2 2 2h8c1.1 0 2-.9 2-2V7H6v12zM19 4h-3.5l-1-1h-5l-1 1H5v2h14V4z"/></svg>' +
                      'Yes, Delete' +
                    '</button>';
            }
            return;
        }

        // Regular users: show pending notice if request already submitted
        if (block.getAttribute('data-delete-pending') === 'true') {
            if (title) title.textContent = 'Request Already Submitted';
            if (desc) desc.innerHTML = 'A delete request for <strong>TYPE ' + t + '</strong> has already been submitted and is currently pending Admin/Manager review.';
            if (btns) {
                btns.innerHTML =
                    '<button type="button" class="del-btn-cancel" onclick="closeDelTypeModal()">' +
                      'Close' +
                    '</button>';
            }
            return;
        }

        // Regular users: for saved DB records, submit delete request for review
        if (dbId > 0) {
            if (title) title.textContent = 'Request Deletion of TYPE ' + t + '?';
            if (desc) desc.innerHTML = 'This specification is saved in the database. Deleting it requires <strong>Admin/Manager approval</strong>. A delete request will be submitted for review.';
            if (btns) {
                btns.innerHTML =
                    '<button type="button" class="del-btn-cancel" onclick="closeDelTypeModal()">' +
                      '<svg width="17" height="17" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2.2"><circle cx="12" cy="12" r="10"/><line x1="15" y1="9" x2="9" y2="15"/><line x1="9" y1="9" x2="15" y2="15"/></svg>' +
                      'Cancel' +
                    '</button>' +
                    '<button type="button" class="del-btn-confirm" id="del-confirm-btn" onclick="confirmDeleteType(true)" style="background:#ea580c;border-color:#c2410c;">' +
                      '<svg width="17" height="17" viewBox="0 0 24 24" fill="currentColor"><path d="M19 3H5c-1.1 0-2 .9-2 2v14c0 1.1.9 2 2 2h14c1.1 0 2-.9 2-2V5c0-1.1-.9-2-2-2zm0 10h-4c0 1.66-1.34 3-3 3s-3-1.34-3-3H5V5h14v8z"/></svg>' +
                      'Submit Delete Request' +
                    '</button>';
            }
        }
    };

    // Override Delete Confirmation
    window.confirmDeleteType = async function (forceRequest) {
        if (typeof pendingDeleteTypeId === 'undefined' || pendingDeleteTypeId === null) return;
        var t = pendingDeleteTypeId;
        var block = document.getElementById('wps-type-block-' + t);
        if (!block) {
            closeDelTypeModal();
            return;
        }

        var dbId = parseInt(block.getAttribute('data-id') || '0', 10);
        var controller = getControllerName();
        var masterName = getMasterDisplayName();

        var typeInp = document.getElementById('wps-type-' + t);
        var rawTypeName = typeInp ? typeInp.value.trim() : '';

        var isPrivileged = window.currentUser && (window.currentUser.isAdmin || window.currentUser.isManager);
        var isRequestFlow = !isPrivileged && (dbId > 0);

        if (!isPrivileged && block.getAttribute('data-delete-pending') === 'true') {
            showActionToast('info', {
                masterName: masterName,
                message: 'Request already submitted. A delete request for this specification is already pending review.'
            });
            closeDelTypeModal();
            return;
        }

        if (dbId > 0 && isRequestFlow) {
            try {
                var response = await fetch('/DeleteRequests/Create', {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify({
                        masterName: masterName,
                        controllerName: controller,
                        recordId: dbId,
                        typeIndex: t,
                        typeName: rawTypeName || ('TYPE ' + t),
                        actionUrl: window.location.pathname
                    })
                });
                var result = await response.json();
                if (!response.ok || !result.success) {
                    showActionToast('info', {
                        masterName: masterName,
                        message: result.message || 'Request already submitted. A delete request is already pending review.'
                    });
                    block.setAttribute('data-delete-pending', 'true');
                    var h = block.querySelector('.wps-type-header') || block.querySelector('.card-header');
                    if (h && !h.querySelector('.req-pending-pill')) {
                        var p = document.createElement('span');
                        p.className = 'req-pending-pill';
                        p.textContent = 'Deletion Pending Approval';
                        h.appendChild(p);
                    }
                    closeDelTypeModal();
                    return;
                }
                showActionToast('info', {
                    masterName: masterName,
                    message: 'Delete request submitted for Admin/Manager approval.'
                });

                // Mark block as pending approval
                block.setAttribute('data-delete-pending', 'true');
                var header = block.querySelector('.wps-type-header') || block.querySelector('.card-header');
                if (header && !header.querySelector('.req-pending-pill')) {
                    var pill = document.createElement('span');
                    pill.className = 'req-pending-pill';
                    pill.textContent = 'Deletion Pending Approval';
                    header.appendChild(pill);
                }
                if (typeof loadDeleteRequests === 'function') {
                    loadDeleteRequests();
                }
            } catch (err) {
                showActionToast('error', {
                    masterName: masterName,
                    message: err.message || 'Network error submitting delete request.'
                });
            }
            closeDelTypeModal();
            return;
        }

        if (dbId > 0) {
            try {
                var response = await fetch('/' + controller + '/DeleteType', {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify({ id: dbId })
                });
                var result = await response.json();
                if (!response.ok || !result.success) {
                    showActionToast('error', {
                        masterName: masterName,
                        message: result.message || 'Failed to delete record.'
                    });
                    closeDelTypeModal();
                    return;
                }
                showActionToast('deleted', {
                    masterName: masterName,
                    typeName: rawTypeName,
                    typeIndex: t
                });
            } catch (err) {
                showActionToast('error', {
                    masterName: masterName,
                    message: err.message || 'Network error deleting record.'
                });
                closeDelTypeModal();
                return;
            }
        } else {
            // Block deleted before initial DB save
            showActionToast('deleted', {
                masterName: masterName,
                typeName: rawTypeName,
                typeIndex: t
            });
        }

        if (block.parentNode) block.parentNode.removeChild(block);
        closeDelTypeModal();

        if (typeof renumberAllTypes === 'function') {
            renumberAllTypes();
        }

        if (typeof loadDeleteRequests === 'function') {
            loadDeleteRequests();
        }
        if (typeof syncPendingDeleteRequests === 'function') {
            syncPendingDeleteRequests();
        }

        // Re-sync all remaining blocks and their action buttons
        document.querySelectorAll('.wps-type-block').forEach(function (b, idx) {
            var newT = idx + 1;
            var isSaved = b.classList.contains('type-locked') || (!b.classList.contains('type-editing') && parseInt(b.getAttribute('data-id') || '0', 10) > 0);
            if (isSaved) {
                setBlockEditable(newT, false);
                renderTypeButton(newT, true);
            } else {
                setBlockEditable(newT, true);
                renderTypeButton(newT, false);
            }
        });

        var remaining = document.querySelectorAll('.wps-type-block').length;
        if (remaining === 0 && typeof addWpsType === 'function') {
            addWpsType(false);
        }

        if (window.cachedMasterRecords && dbId > 0) {
            window.cachedMasterRecords = window.cachedMasterRecords.filter(function (r) { return r.id !== dbId; });
        }

        if (typeof detectVisibleType === 'function') detectVisibleType();
        if (typeof updateFloatingDock === 'function') updateFloatingDock();
    };

    // Populate a block `t` with saved record data
    function populateBlockWithData(t, record) {
        var block = document.getElementById('wps-type-block-' + t);
        if (!block) return;

        block.setAttribute('data-id', record.id);

        var typeInput = document.getElementById('wps-type-' + t);
        if (typeInput) typeInput.value = record.typeName || '';

        var specInput = document.getElementById('wps-spec-' + t);
        if (specInput) specInput.value = record.systemDescription || '';

        var lenInput = document.getElementById('wps-len-' + t);
        if (lenInput) lenInput.value = record.dimensionLength > 0 ? record.dimensionLength : '';

        var htInput = document.getElementById('wps-ht-' + t);
        if (htInput) htInput.value = record.dimensionHeight > 0 ? record.dimensionHeight : '';

        var areaEl = document.getElementById('wps-area-' + t);
        if (areaEl) areaEl.textContent = (record.dimensionArea || 0).toFixed(2);

        // Populate Material Costs
        var matTbody = document.getElementById('mat-tbody-' + t);
        populateCostRows(matTbody, record.materialCosts, t, 'Material');
        if (typeof recalcMatTotalsFor === 'function') recalcMatTotalsFor(t);

        // Populate Labour Costs
        var labTbody = document.getElementById('lab-tbody-' + t);
        populateCostRows(labTbody, record.labourCosts, t, 'Labour');
        if (typeof recalcLabTotalsFor === 'function') recalcLabTotalsFor(t);

        // Populate Profit percentages and remarks
        var pctT = document.getElementById('pct-transport-' + t);
        if (pctT) {
            pctT.value = record.transportationPercent;
            var rRow = pctT.closest('tr');
            if (rRow) {
                var rem = rRow.querySelector('.profit-remark');
                if (rem) rem.value = record.transportationRemarks || '';
            }
        }

        var pctW = document.getElementById('pct-water-' + t);
        if (pctW) {
            pctW.value = record.waterElectricityPercent;
            var rRow = pctW.closest('tr');
            if (rRow) {
                var rem = rRow.querySelector('.profit-remark');
                if (rem) rem.value = record.waterElectricityRemarks || '';
            }
        }

        var pctTools = document.getElementById('pct-tools-' + t);
        if (pctTools) {
            pctTools.value = record.toolsEquipmentPercent;
            var rRow = pctTools.closest('tr');
            if (rRow) {
                var rem = rRow.querySelector('.profit-remark');
                if (rem) rem.value = record.toolsEquipmentRemarks || '';
            }
        }

        var pctOH = document.getElementById('pct-oh-' + t);
        if (pctOH) pctOH.value = record.profitPercent;

        if (typeof recalcProfit === 'function') recalcProfit(t);

        // Display created / updated audit bar for saved record
        if (record.createdBy || record.createdOn || record.updatedBy || record.updatedOn) {
            updateAuditBar(t, record);
        }

        // Lock block and set button to Edit mode for saved record
        setBlockEditable(t, false);
        renderTypeButton(t, true);
        block._originalSnapshot = JSON.stringify(extractComparablePayload(t));
    }

    // Load all saved records on page ready
    async function loadSavedRecords() {
        var controller = getControllerName();
        // Ignore non-master controllers like Home, Account
        if (controller === 'Home' || controller === 'Account') {
            initFloatingDock();
            return;
        }

        try {
            var response = await fetch('/' + controller + '/GetAll');
            if (!response.ok) {
                initFloatingDock();
                return;
            }

            var records = await response.json();
            if (Array.isArray(records)) {
                window.cachedMasterRecords = records;
            }

            if (Array.isArray(records) && records.length > 0) {
                // Remove any default empty blocks created on initial load
                var container = document.getElementById('wps-types-container');
                if (container) container.innerHTML = '';
                if (typeof wpsCount !== 'undefined') wpsCount = 0;
                if (typeof matRowCounts !== 'undefined') matRowCounts = {};
                if (typeof labRowCounts !== 'undefined') labRowCounts = {};

                // Render each record
                records.forEach(function (rec, index) {
                    var t = index + 1;
                    if (typeof addWpsType === 'function') {
                        addWpsType(false);
                        populateBlockWithData(t, rec);
                    }
                });
            }
        } catch (e) {
            console.warn('Could not load saved master records:', e);
        }

        initFloatingDock();
    }

    // -------------------------------------------------------------
    // Floating Quick Actions Dock on Right Screen Edge
    // Automatically binds to whichever TYPE is visible/active
    // -------------------------------------------------------------
    var currentActiveDockType = 1;

    function getOrCreateFloatingDock() {
        var dock = document.getElementById('smard-floating-dock');
        if (!dock) {
            dock = document.createElement('div');
            dock.id = 'smard-floating-dock';
            dock.className = 'smard-floating-dock';
            dock.innerHTML =
                '<div class="dock-type-label" id="dock-active-type-lbl">TYPE 1</div>' +
                '<div class="dock-actions-container" id="dock-actions-container"></div>';
            document.body.appendChild(dock);
        }
        return dock;
    }

    function isBlockInEditMode(t) {
        var block = document.getElementById('wps-type-block-' + t);
        if (!block) return false;
        return !block.classList.contains('type-locked') || block.classList.contains('type-editing');
    }

    function updateFloatingDock() {
        var blocks = document.querySelectorAll('.wps-type-block');
        var dock = getOrCreateFloatingDock();
        if (blocks.length === 0) {
            dock.style.display = 'none';
            return;
        }

        dock.style.display = 'flex';

        // Check if currentActiveDockType still exists in DOM
        var currentBlock = document.getElementById('wps-type-block-' + currentActiveDockType);
        if (!currentBlock) {
            var firstBlock = blocks[0];
            var parsed = parseInt(firstBlock.id.replace('wps-type-block-', ''), 10);
            currentActiveDockType = isNaN(parsed) ? 1 : parsed;
            currentBlock = document.getElementById('wps-type-block-' + currentActiveDockType);
        }

        var lbl = document.getElementById('dock-active-type-lbl');
        if (lbl) {
            lbl.textContent = 'TYPE ' + currentActiveDockType;
        }

        var container = document.getElementById('dock-actions-container');
        if (!container) return;

        var isEdit = isBlockInEditMode(currentActiveDockType);

        if (isEdit) {
            // Edit State: Save (Green) + Cancel (Slate) + Delete (Red)
            container.innerHTML =
                '<button type="button" class="dock-btn dock-btn-save" onclick="onDockSaveClick()" title="Save this type" aria-label="Save this type">' +
                    '<svg viewBox="0 0 24 24" width="16" height="16" fill="none" stroke="#FFFFFF" stroke-width="2.2" stroke-linecap="round" stroke-linejoin="round">' +
                        '<path d="M19 21H5a2 2 0 0 1-2-2V5a2 2 0 0 1 2-2h11l5 5v11a2 2 0 0 1-2 2z"></path>' +
                        '<polyline points="17 21 17 13 7 13 7 21"></polyline>' +
                        '<polyline points="7 3 7 8 15 8"></polyline>' +
                    '</svg>' +
                    '<span class="dock-tooltip">Save this type</span>' +
                '</button>' +
                '<button type="button" class="dock-btn dock-btn-cancel" onclick="onDockCancelClick()" title="Cancel editing" aria-label="Cancel editing">' +
                    '<svg viewBox="0 0 24 24" width="16" height="16" fill="none" stroke="#FFFFFF" stroke-width="2.2" stroke-linecap="round" stroke-linejoin="round">' +
                        '<circle cx="12" cy="12" r="10"></circle>' +
                        '<line x1="15" y1="9" x2="9" y2="15"></line>' +
                        '<line x1="9" y1="9" x2="15" y2="15"></line>' +
                    '</svg>' +
                    '<span class="dock-tooltip">Cancel</span>' +
                '</button>' +
                '<button type="button" class="dock-btn dock-btn-delete" onclick="onDockDeleteClick()" title="Delete this type" aria-label="Delete this type">' +
                    '<svg viewBox="0 0 24 24" width="16" height="16" fill="currentColor">' +
                        '<path d="M6 19c0 1.1.9 2 2 2h8c1.1 0 2-.9 2-2V7H6v12zM19 4h-3.5l-1-1h-5l-1 1H5v2h14V4z"></path>' +
                    '</svg>' +
                    '<span class="dock-tooltip">Delete this type</span>' +
                '</button>';
        } else {
            // View / Locked State: Edit (Blue) + Delete (Red)
            container.innerHTML =
                '<button type="button" class="dock-btn dock-btn-edit" onclick="onDockEditClick()" title="Edit this type" aria-label="Edit this type">' +
                    '<svg viewBox="0 0 24 24" width="16" height="16" fill="none" stroke="#FFFFFF" stroke-width="2.3" stroke-linecap="round" stroke-linejoin="round">' +
                        '<path d="M11 4H4a2 2 0 0 0-2 2v14a2 2 0 0 0 2 2h14a2 2 0 0 0 2-2v-7"></path>' +
                        '<path d="M18.5 2.5a2.121 2.121 0 0 1 3 3L12 15l-4 1 1-4 9.5-9.5z"></path>' +
                    '</svg>' +
                    '<span class="dock-tooltip">Edit this type</span>' +
                '</button>' +
                '<button type="button" class="dock-btn dock-btn-delete" onclick="onDockDeleteClick()" title="Delete this type" aria-label="Delete this type">' +
                    '<svg viewBox="0 0 24 24" width="16" height="16" fill="currentColor">' +
                        '<path d="M6 19c0 1.1.9 2 2 2h8c1.1 0 2-.9 2-2V7H6v12zM19 4h-3.5l-1-1h-5l-1 1H5v2h14V4z"></path>' +
                    '</svg>' +
                    '<span class="dock-tooltip">Delete this type</span>' +
                '</button>';
        }
    }
    window.updateFloatingDock = updateFloatingDock;

    // Floating dock button actions
    window.onDockEditClick = function () {
        if (!currentActiveDockType) return;
        window.editType(currentActiveDockType);
        updateFloatingDock();
    };

    window.onDockSaveClick = function () {
        if (!currentActiveDockType) return;
        window.saveType(currentActiveDockType);
    };

    window.onDockCancelClick = function () {
        if (!currentActiveDockType) return;
        window.cancelEditType(currentActiveDockType);
    };

    window.onDockDeleteClick = function () {
        if (!currentActiveDockType) return;
        if (typeof deleteType === 'function') {
            deleteType(currentActiveDockType);
        }
    };

    // Cancel edit mode: revert to last saved state or view mode
    window.cancelEditType = function (t) {
        var block = document.getElementById('wps-type-block-' + t);
        if (!block) return;

        var dbId = parseInt(block.getAttribute('data-id') || '0', 10);
        if (dbId > 0 && Array.isArray(window.cachedMasterRecords)) {
            var rec = window.cachedMasterRecords.find(function (r) { return r.id === dbId; });
            if (rec) {
                populateBlockWithData(t, rec);
            }
        }

        setBlockEditable(t, false);
        renderTypeButton(t, true);
        updateFloatingDock();
        showToast('Notice', 'Editing cancelled for TYPE ' + t + '.', 'info');
    };

    // Auto-detect which type is currently visible in viewport
    function detectVisibleType() {
        var blocks = document.querySelectorAll('.wps-type-block');
        if (blocks.length === 0) {
            updateFloatingDock();
            return;
        }

        var vpHeight = window.innerHeight;
        var vpCenter = vpHeight * 0.45;
        var bestBlock = null;
        var bestScore = -Infinity;

        blocks.forEach(function (b) {
            var rect = b.getBoundingClientRect();
            var visibleTop = Math.max(0, rect.top);
            var visibleBottom = Math.min(vpHeight, rect.bottom);
            var visibleHeight = Math.max(0, visibleBottom - visibleTop);
            var containsCenter = (rect.top <= vpCenter && rect.bottom >= vpCenter);
            var score = visibleHeight + (containsCenter ? 10000 : 0);

            if (score > bestScore) {
                bestScore = score;
                bestBlock = b;
            }
        });

        if (bestBlock) {
            var t = parseInt(bestBlock.id.replace('wps-type-block-', ''), 10);
            if (!isNaN(t) && t !== currentActiveDockType) {
                currentActiveDockType = t;
                updateFloatingDock();
            }
        }
    }
    window.detectVisibleType = detectVisibleType;

    // Scroll listeners (throttled with rAF)
    var scrollScheduled = false;
    function onScrollCheck() {
        if (!scrollScheduled) {
            scrollScheduled = true;
            requestAnimationFrame(function () {
                detectVisibleType();
                scrollScheduled = false;
            });
        }
    }

    function initFloatingDock() {
        getOrCreateFloatingDock();
        detectVisibleType();
        updateFloatingDock();

        // Listen to scroll events on window, document, and .dash-main-wrapper
        window.addEventListener('scroll', onScrollCheck, { passive: true });
        document.addEventListener('scroll', onScrollCheck, { passive: true });
        var mainWrap = document.querySelector('.dash-main-wrapper');
        if (mainWrap) {
            mainWrap.addEventListener('scroll', onScrollCheck, { passive: true });
        }

        // When user clicks or focuses inside any type block, immediately make it active
        document.addEventListener('click', function (e) {
            var block = e.target.closest('.wps-type-block');
            if (block) {
                var t = parseInt(block.id.replace('wps-type-block-', ''), 10);
                if (!isNaN(t) && t !== currentActiveDockType) {
                    currentActiveDockType = t;
                    updateFloatingDock();
                }
            }
        }, { passive: true });

        document.addEventListener('focusin', function (e) {
            var block = e.target.closest('.wps-type-block');
            if (block) {
                var t = parseInt(block.id.replace('wps-type-block-', ''), 10);
                if (!isNaN(t) && t !== currentActiveDockType) {
                    currentActiveDockType = t;
                    updateFloatingDock();
                }
            }
        }, { passive: true });
    }
    window.initFloatingDock = initFloatingDock;

    function syncPendingDeleteRequests() {
        var controller = getControllerName();
        fetch('/DeleteRequests/GetPendingForMaster?controller=' + encodeURIComponent(controller))
            .then(function(res) { if (!res.ok) return null; return res.json(); })
            .then(function(data) {
                var pendingIds = (data && data.recordIds) ? data.recordIds : [];
                document.querySelectorAll('.wps-type-block').forEach(function(block) {
                    var dbId = parseInt(block.getAttribute('data-id') || '0', 10);
                    if (dbId > 0 && pendingIds.indexOf(dbId) !== -1) {
                        block.setAttribute('data-delete-pending', 'true');
                        var header = block.querySelector('.wps-card-header, .wps-type-header, .card-header');
                        if (header && !header.querySelector('.req-pending-pill')) {
                            var pill = document.createElement('span');
                            pill.className = 'req-pending-pill';
                            pill.textContent = 'Deletion Pending Approval';
                            header.appendChild(pill);
                        }
                    } else if (dbId > 0 && pendingIds.indexOf(dbId) === -1) {
                        block.removeAttribute('data-delete-pending');
                        var pill = block.querySelector('.req-pending-pill');
                        if (pill) pill.remove();
                    }
                });
            })
            .catch(function() {});
    }
    window.syncPendingDeleteRequests = syncPendingDeleteRequests;

    async function loadMasterRecordsFromServer() {
        var container = document.getElementById('wps-types-container');
        if (!container) return; // Not a master page

        var records = null;
        if (Array.isArray(window.initialMasterRecords)) {
            records = window.initialMasterRecords;
            window.initialMasterRecords = null; // consume once
        } else {
            var controller = getControllerName();
            try {
                var response = await fetch('/' + controller + '/GetAll');
                if (response.ok) {
                    records = await response.json();
                }
            } catch (err) {
                console.error('Failed to fetch master records:', err);
            }
        }

        if (!Array.isArray(records) || records.length === 0) {
            window.cachedMasterRecords = [];
            if (typeof initFloatingDock === 'function') initFloatingDock();
            return;
        }

        window.cachedMasterRecords = records;

        // Clear initial blank block and reset type/row counters
        container.innerHTML = '';
        if (typeof window.wpsCount !== 'undefined') window.wpsCount = 0;
        if (typeof wpsCount !== 'undefined') wpsCount = 0;
        if (window.matRowCounts) window.matRowCounts = {};
        if (window.labRowCounts) window.labRowCounts = {};

        var addTypeFn = window.addWpsType || (typeof addWpsType === 'function' ? addWpsType : null);
        if (!addTypeFn) {
            console.error('addWpsType function not found on page');
            return;
        }

        // Render and populate each saved record
        records.forEach(function (rec, idx) {
            var t = idx + 1;
            addTypeFn(false);
            populateBlockWithData(t, rec);
        });

        // Sync pending delete requests
        syncPendingDeleteRequests();

        // Initialize floating dock
        if (typeof initFloatingDock === 'function') {
            initFloatingDock();
        }
    }
    window.loadMasterRecordsFromServer = loadMasterRecordsFromServer;

    function initMasterPage() {
        loadMasterRecordsFromServer();
    }

    // Initialize when DOM is loaded
    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', initMasterPage);
    } else {
        initMasterPage();
    }
})();
